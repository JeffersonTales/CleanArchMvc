using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities {
    public sealed class Product : Entity {

        #region Propriedades de Domínio
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        #endregion

        #region Construtores
        public Product(string name, 
                       string description, 
                       decimal price, 
                       int stock, 
                       string image) {
            
            this.ValidateDomain(name, 
                                description, 
                                price, 
                                stock, 
                                image);
        }

        public Product(int id, 
                       string name, 
                       string description, 
                       decimal price, 
                       int stock, 
                       string image) {

            DomainExceptionValidation.When(hasError: id < 0, 
                                           error: "Invalid Id value");
            this.Id = id;
            this.ValidateDomain(name,
                                description,
                                price,
                                stock,
                                image);
        }
        #endregion

        #region Métodos
        public void Update(string name,
                           string description,
                           decimal price,
                           int stock,
                           string image,
                           int categoryId) {

            this.ValidateDomain(name,
                                description,
                                price,
                                stock,
                                image);

            this.CategoryId = categoryId; 
        }
        #endregion

        #region Validações
        private void ValidateDomain(string name,
                                    string description,
                                    decimal price,
                                    int stock,
                                    string image) {

            DomainExceptionValidation.When(hasError: string.IsNullOrEmpty(name),
                                           error: "Invalid name. Name is required");

            DomainExceptionValidation.When(hasError: name.Length < 3,
                                           error: "Invalid name, too short, minimum 3 characters");

            DomainExceptionValidation.When(hasError: string.IsNullOrEmpty(description),
                                           error: "Invalid description. Description is required");

            DomainExceptionValidation.When(hasError: description.Length < 5,
                                           error: "Invalid description, too short, minimum 5 characters");

            DomainExceptionValidation.When(hasError: price < 0,
                                           error: "Invalid price value");

            DomainExceptionValidation.When(hasError: stock < 0,
                                           error: "Invalid stock value");

            DomainExceptionValidation.When(hasError: image?.Length > 250,
                                           error: "Invalid image name, too long, maximum 250 characters");

            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Stock = stock;
            this.Image = image;
        }
        #endregion
    }
}
