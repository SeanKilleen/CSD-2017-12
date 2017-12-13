Feature: Release Change
	In order to not waste money
	As a vending machine customer
	I want to get correct change back

Scenario: Get Correct Change Back
	Given I have inserted a quarter
	And I have inserted a quarter
	When I release the change
	Then I should receive 50 cents
