Feature: CommonServices
	In order to use the browser
	As a automation engineer
	I want BELLATRIX to provide me handy method to do my job

Background: 
Given I use Chrome browser on Windows
And I reuse the browser if started
And I take a screenshot for failed tests
And I record a video for failed tests
And I open browser

Scenario: Browser Service Common Steps
	When I navigate to URL http://demos.bellatrix.solutions/product/falcon-9/
	And I refresh the browser
	When I wait until the browser is ready
	And I wait for all AJAX requests to finish
	And I maximize the browser
	And I navigate to URL http://demos.bellatrix.solutions/
	And I click browser's back button
	And I click browser's forward button
    And I click browser's back button
	And I wait for partial URL falcon-9

Scenario: Cookies Service Common Steps
	When I navigate to URL http://demos.bellatrix.solutions/product/falcon-9/
	And I add cookie name = testCookie value = 99
    And I delete cookie testCookie
    And I add cookie name = testCookie1 value = 100
    And I delete all cookies