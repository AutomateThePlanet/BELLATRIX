namespace Bellatrix.Web.Tests;

public partial class HomePage : WebPage
{
    public override string Url => "https://demos.bellatrix.solutions/";

    public void FilterProducts(ProductFilter productFilter)
    {
        switch (productFilter)
        {
            case ProductFilter.Popularity:
                SortDropDown.SelectByText("Sort by popularity");
                break;
            case ProductFilter.Rating:
                SortDropDown.SelectByText("Sort by average rating");
                break;
            case ProductFilter.Date:
                SortDropDown.SelectByText("Sort by newness");
                break;
            case ProductFilter.Price:
                SortDropDown.SelectByText("Sort by price: low to high");
                break;
            case ProductFilter.PriceDesc:
                SortDropDown.SelectByText("Sort by price: high to low");
                break;
        }
    }

    public void AddProductById(int productId)
    {
        var product = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", productId.ToString()).ToBeClickable();
        product.Click();
        ViewCartButton.ValidateIsVisible();
    }
}