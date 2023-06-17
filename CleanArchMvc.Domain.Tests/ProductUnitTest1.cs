using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchMvc.Domain.Tests {
    public class ProductUnitTest1 {

        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState() {

            Action action = () => new Product(id:1,
                                              name:"Product Name",
                                              description:"Product Description",
                                              price:9.99m,
                                              stock:99,
                                              image: "product image");

            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId() {

            Action action = () => new Product(id: -1,
                                              name: "Product Name",
                                              description: "Product Description",
                                              price: 9.99m,
                                              stock: 99,
                                              image: "product image");

            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid Id value");

        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName() {

            Action action = () => new Product(id: 1,
                                              name: "Pr",
                                              description: "Product Description",
                                              price: 9.99m,
                                              stock: 99,
                                              image: "product image");

            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum 3 characters");

        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName() {

            Action action = () => new Product(id: 1,
                                              name: "Product Name",
                                              description: "Product Description",
                                              price: 9.99m,
                                              stock: 99,
                                              image: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid image name, too long, maximum 250 characters");

        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException() {

            Action action = () => new Product(id: 1,
                                              name: "Product Name",
                                              description: "Product Description",
                                              price: 9.99m,
                                              stock: 99,
                                              image: null);

            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoNullReferenceException() {

            Action action = () => new Product(id: 1,
                                              name: "Product Name",
                                              description: "Product Description",
                                              price: 9.99m,
                                              stock: 99,
                                              image: null);

            action.Should().NotThrow<NullReferenceException>();

        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException() {

            Action action = () => new Product(id: 1,
                                              name: "Product Name",
                                              description: "Product Description",
                                              price: 9.99m,
                                              stock: 99,
                                              image: "");

            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value) {

            Action action = () => new Product(id: 1,
                                             name: "Product Name",
                                             description: "Product Description",
                                             price: 9.99m,
                                             stock: value,
                                             image: "product image");

            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid stock value");

        }

        [Theory]
        [InlineData(-1.00)]
        public void CreateProduct_InvalidPriceValue_ExceptionDomainNegativeValue(int value) {

            Action action = () => new Product(id: 1,
                                             name: "Product Name",
                                             description: "Product Description",
                                             price: value,
                                             stock: 99,
                                             image: "product image");

            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid price value");

        }

    }
}
