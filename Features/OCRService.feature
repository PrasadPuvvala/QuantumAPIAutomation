Feature: OCRService

A short summary of the feature

@1769058
Scenario: 01Test Case ID 1769058: Verify that the OCR service returns a list of all identified character strings from the image provided
	
	When Send the request with a correct image as input
	When Verify the response
	Then The response must contain a list of all the identified character strings.
