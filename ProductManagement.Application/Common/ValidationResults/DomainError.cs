using Extensions.Web.Results;

namespace ProductManagement.Application.Common.ValidationResults;

public static class DomainErrors
{
    public static class Categories
    {
        public static readonly Error CreateCategoryFailed = new(
            "Category.Create",
            "Category creation failed"
        );
        
        public static readonly Error DeleteCategoryFailed = new(
            "Category.Delete",
            "Category deletion failed"
        );
        public static readonly Error UpdateCategoryFailed = new(
            "Category.Update",
            "Category updating failed"
        );
        public static readonly Error CategoryNotFound = new(
            "Category.GetById",
            "Category was not found by provided id"
        );
    }
    
    public static class Login
    {
        public static readonly Error UserNamesNotRegistered = new(
            "Authentication.Login",
            "Provided username is not registered in the system"
        );
        
        public static readonly Error InvalidCredentialsProvided = new(
            "Authentication.Login",
            "Provided credentials are invalid"
        );
    }
    
    public static class Registration
    {
        public static readonly Error UserNameAlreadyExists = new(
            "Authentication.Registration",
            "Provided username is already exists in the system"
        );
        
        public static readonly Error UserCreationFailed = new(
            "Authentication.Registration",
            "User registration failed"
        );
    }
    
    public static class Products
    {
        public static readonly Error ProductCreationFailed = new(
            "Products.Create",
            "Product creation failed"
        );
        
        public static readonly Error ProductDeletionFailed = new(
            "Products.Delete",
            "Product deletion failed"
        );
        
        public static readonly Error ProductUpdatingFailed = new(
            "Products.Update",
            "Product updating failed"
        );
        
        public static readonly Error NotFound = new(
            "Products.Get",
            "Product not found"
        );
    }
}