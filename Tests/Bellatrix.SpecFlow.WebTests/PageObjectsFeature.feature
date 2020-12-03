Feature: Navigate to BELLATRIX Online Rocket Shop
	To purchase a new rocket
	As a Nuclear Engineer 
	I want to be able to buy a new rocket.

Background: 
Given I use Chrome browser on Windows
And I restart the browser on test fail
And I record a video for failed tests
And I take a screenshot for failed tests 
And I open browser

Scenario: Successfully By Product 28 with Coupon
	Given An Angular test works


Scenario: Successfully By Product 28 with Coupon1
	
	When I navigate to home page
	And I filter products by popularity
	And I add product by ID = 28
	And I click view cart button
	And I apply coupon happybirthday
	Then I assert total price is equal to 60.00
    When I click proceed to checkout button
    And I set first name = In
    And I set last name = Deepthought
    And I set company = Automate The Planet Ltd.
    And I set country = Bulgaria
    And I set address 1 = bul. Yerusalim 5
    And I set address 2 = bul. Yerusalim 6
    And I set city = Sofia
    And I set state = Sofia-Grad
    And I set zip = 1000
    And I set phone = +00359894646464
    And I set email = info@bellatrix.solutions
    And I add  order comments = cool product
    And I check payments button
