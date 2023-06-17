using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities {
    public sealed class Category : Entity {

        #region Propriedades de Domínio
        public string Name { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public ICollection<Product> Products { get; set; }
        #endregion

        #region Construtores
        public Category(string name) {

            this.ValidateDomain(name);
 
        }

        public Category(int id, 
                        string name) {

            DomainExceptionValidation.When(hasError: id < 0, 
                                           error: "Invalid Id value");
            this.Id = id;
            this.ValidateDomain(name);
        }
        #endregion

        #region Métodos
        public void Update(string name) {
            this.ValidateDomain(name);
        }
        #endregion

        #region Validações
        private void ValidateDomain(string name) {

            DomainExceptionValidation.When(hasError: string.IsNullOrEmpty(name), 
                                           error: "Invalid name. Name is required");

            DomainExceptionValidation.When(hasError: name.Length < 3,
                                           error: "Invalid name, too short, minimun 3 characters");

            this.Name = name;
        }
        #endregion
    }
}
