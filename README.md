# CellularAutomata
Conway's Game of life
 
A bare bones NET 6 implementation of Conway's Game of Life.
 
This version contain two Rulesets.  The first Ruleset is the standard Conway rules for Life.  The second Ruleset, McCarron, modifies Conway's rules by biasing the death of cells.  Even a small bias leads to growth much like that observed in Bacteria.  The McCarron ruleset allows exploration of emergent and complex organisation.
 
UI Explanation 
 
Ruleset - This menu item defines which rules to use.  Available rules are Conway and McCarron. 
 
Board Settings 
Starting Density - This menu item defines how dense the initial conditions are. Larger values are more sparse. 
Board Seed - This menu item defines the seed used randomly fill the board initially.  The same seed will produce the same board each time.  There are 2147483647 possiblities. 
 
McCarron Settings 
These menu items only effect the McCarron ruleset.
 
Ruleset seed - This menu item defines the seed used by the Death Bias.  The same seed allows repeatability.  There are 2147483647 possiblities. 
Death Bias - This menu item defines how often a cell can escape death.  The standard setting of 100 means 2% and higher values means lower percentages.

