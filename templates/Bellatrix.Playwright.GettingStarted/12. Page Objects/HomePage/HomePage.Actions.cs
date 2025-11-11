namespace Bellatrix.Playwright.GettingStarted._12._Page_Objects;

public partial class HomePage : WebPage
{
    public override string Url => "https://demos.bellatrix.solutions/";

    // 1. Filter by using the special enum containing all filter options.
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

    // 2. Generic method for adding products to the cart by ID.
    public void AddProductById(int productId)
    {
        var product = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", productId.ToString()).ToBeClickable();
        product.Click();
        ViewCartButton.ValidateIsVisible();
    }
}