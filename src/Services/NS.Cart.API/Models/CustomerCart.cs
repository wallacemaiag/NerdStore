using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NS.Cart.API.Models
{
    public class CustomerCart
    {
        internal const int MAX_QUANTITY_ITEM = 5;
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
        public List<CartItem> Itens { get; set; } = new List<CartItem>();
        public ValidationResult ValidationResult { get; set; }

        public CustomerCart(Guid clientId)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
        }

        public CustomerCart() { }

        internal bool IsValid()
        {
            var errors = Itens.SelectMany(i => new CartItem.CartItemValidation().Validate(i).Errors).ToList();
            errors.AddRange(new CustomerCartValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

        internal void CalculateValueCart()
        {
            Amount = Itens.Sum(p => p.CalculateValue());
        }

        internal bool ExistingItemCart(CartItem item)
        {
            return Itens.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetProductById(Guid id)
        {
            return Itens.FirstOrDefault(p => p.ProductId == id);
        }

        internal void AddItem(CartItem item)
        {
            item.AssociateCart(Id);

            if (ExistingItemCart(item))
            {
                var existingItem = GetProductById(item.ProductId);
                existingItem.AddUnit(item.Quantity);

                item = existingItem;
                Itens.Remove(existingItem);
            }

            Itens.Add(item);
            CalculateValueCart();
        }

        internal void UpdateItem(CartItem item)
        {
            item.AssociateCart(Id);

            var existingItem = GetProductById(item.ProductId);

            Itens.Remove(existingItem);
            Itens.Add(item);

            CalculateValueCart();
        }

        internal void UpdateUnit(CartItem item, int unit)
        {
            item.UpdateUnit(unit);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            Itens.Remove(GetProductById(item.ProductId));

            CalculateValueCart();
        }
    }

    public class CustomerCartValidation : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido!");

            RuleFor(c => c.Itens.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");

            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho deve ser maior que 0 (zero)");
        }
    }

}

