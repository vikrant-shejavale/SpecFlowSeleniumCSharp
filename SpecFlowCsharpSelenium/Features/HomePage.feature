Feature: Home Page

A short summary of the feature

@testId
Scenario: Homepage Cart test
	Given I click on "RIDE" tab on home page
	When I add product on position "1" to cart
	Given I click on "RIDE" tab on home page
	When I add product on position "3" to cart
	And I remove product at position 1 from cart
	And I click View and Edit Cart link
	And I change quatity for 1st item in cart to 5
	Then I verify total price is updated after changing quantity
	And I click on Proceed to Checkout button
	And I navigate to Checkout page
	And I click PAY NOW button
	And I verify Error message dispalyed "This is a required field."
	And I change shipping method to "Express Shipping"
	Then I verify shipping price is updated