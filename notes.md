Notes
	+ On module load & city selection changed
		+ Set default symbology
			+ All neighborhoods: unique values
			+ All neighborhoods: grey
	+ On neighborhood selection changed
		+ Selected neighborhood receives its associated unique color
			- How to associate these colors
				+ Dictionary?
				+ Use color ramp to generate values at startup/on neighborhood addition?
					- CIMFixedColorRamp
			- Need to be synchronized across 2 different maps
			- Needs to allow for additional neighborhoods to be added
			- Needs to look good

	+ Zoom functionality broken on symbology branch
		+ Remove an await in view code behind neighborhood selection changed event handler
		+ Type conversion of eventArgs items
	+ To integrate data from other cities
		+ Create two maps (one inset and one neighborhood focus) 
			- Using the established naming conventions
				- CityName Neighborhoods, CityName Inset
		- Layer fetching will have to be reworked to allow for neighborhoods stored in seperate layers
		+ Listing neighborhood names will not require a row cursor
			- How to determine if a layer is a neighborhood layer
				+ It contains census block or tract related fields
		+ Indexing (use position in Map's layers enumerable)	
		+ Use a fixed color ramp
	+ A binary search method can be used to locate the unique value class in UpdateNeighborhoodSymbology because they are in alphabetical order by name?
	+ On feature layer renderer color ramp property changed
		- set null values to no color
		- set selected neighborhood to index associated color
		- set all other neighborhoods to grey
			
			 
