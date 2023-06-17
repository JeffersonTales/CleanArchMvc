using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests {
    public class CategoryUnitTest1 {

        [Fact (DisplayName ="Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState() {

            Action action = () => new Category(id: 1,name:"Category Name");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>(); 

        }

        [Fact(DisplayName = "Create Category With Invalid Id")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId() {

            Action action = () => new Category(id: -1, name: "Category Name");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid Id value");

        }

        [Fact(DisplayName = "Create Category With Invalid Short Name")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName() {

            Action action = () => new Category(id: 1, name: "Ca");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name, too short, minimun 3 characters");

        }

        [Fact(DisplayName = "Create Category With Invalid Missing Name")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName() {

            Action action = () => new Category(id: 1, name: "");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name. Name is required");

        }

        [Fact(DisplayName = "Create Category With Invalid Null Name")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName() {

            Action action = () => new Category(id: 1, name: null);
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }
    }
}
