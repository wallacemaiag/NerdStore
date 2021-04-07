using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace NS.Cart.API.Models
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public CustomerCart Cart { get; set; }

        public Guid CartId { get; set; }

        internal void AssociateCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal void AddUnit(int unit)
        {
            Quantity += unit;
        }

        internal void UpdateUnit(int unit)
        {
            Quantity = unit;
        }

        internal decimal CalculateValue()
        {
            return Quantity * Value;
        }

        internal bool IsValid()
        {
            return new CartItemValidation().Validate(this).IsValid;
        }

        public class CartItemValidation : AbstractValidator<CartItem>
        {
            public CartItemValidation()
            {
                RuleFor(c => c.ProductId)
                 .NotEqual(Guid.Empty)
                 .WithMessage("Id do produto inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade miníma para o {item.Name} é 1");

                RuleFor(c => c.Quantity)
                    .LessThanOrEqualTo(CustomerCart.MAX_QUANTITY_ITEM)
                    .WithMessage(item => $"A quantidade máxima do {item.Name} é {CustomerCart.MAX_QUANTITY_ITEM}");

                RuleFor(c => c.Value)
                    .GreaterThan(0)
                    .WithMessage(item => $"O valor do {item.Name} precisa ser maior que 0");
            }
        }
    }
}
