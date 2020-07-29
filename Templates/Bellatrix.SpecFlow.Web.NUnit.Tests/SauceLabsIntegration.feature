Feature: SauceLabs Integration
	In order to use the browser in the SauceLabs Cloud
	As a automation engineer
	I want BELLATRIX to provide me handy method to do my job

Background: 
Given I open Chrome browser 68 in SauceLabs
And I want to run the browser on Windows platform
And I want to record a video of the execution
And I want to capture a network logs of the execution
And I want to capture a network logs of the execution
And I want to set build = OrionBeta
And I resize the browser 1200 px x 800 px

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