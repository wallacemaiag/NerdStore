using NS.Core.DomainObjects;
using System;

namespace NS.Clients.API.Models
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Distric { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid ClientId { get; private set; }

        //EF Relations
        public Client Client { get; protected set; }

        public Address(string street, string number, string complement, string distric,
                        string zipCode, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Distric = distric;
            ZipCode = zipCode;
            City = city;
            State = state;
        }
    }
}
