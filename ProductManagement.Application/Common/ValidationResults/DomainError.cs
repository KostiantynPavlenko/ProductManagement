namespace ProductManagement.Application.Common.ValidationResults;

public static class DomainErrors
{
    public static class Categories
    {
        public static readonly Error CreateCategory = new(
            "Category.Create",
            "Category creation failed"
        );
        
        public static readonly Error DeleteCategory = new(
            "Category.Delete",
            "Category deletion failed"
        );
        public static readonly Error UpdateCategory = new(
            "Category.Update",
            "Category updating failed"
        );
        public static readonly Error GetCategory = new(
            "Category.GetById",
            "Category was not found by provided id"
        );
    }
    
    public static class Login
    {
        public static readonly Error UserNameVerification = new(
            "Authentication.Login",
            "Provided username is not registered in the system"
        );
        
        public static readonly Error CredentialsVerification = new(
            "Authentication.Login",
            "Provided credentials are invalid"
        );
    }
    
    public static class Registration
    {
        public static readonly Error UserNameVerification = new(
            "Authentication.Registration",
            "Provided username is already exists in the system"
        );
        
        public static readonly Error UserCreation = new(
            "Authentication.Registration",
            "User registration failed"
        );
    }
    
    public static class Products
    {
        public static readonly Error ProductCreation = new(
            "Products.Create",
            "Product creation failed"
        );
        
        public static readonly Error ProductDeletion = new(
            "Products.Delete",
            "Product deletion failed"
        );
        
        public static readonly Error ProductUpdating = new(
            "Products.Update",
            "Product updating failed"
        );
    }
}