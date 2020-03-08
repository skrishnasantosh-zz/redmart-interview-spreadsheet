# redmart-interview-spreadsheet


###Design Decisions:

- The spreadsheet is implemented as a Graph structure represented by an adaptation of Adjacency Matrix

- Operator handling happens via Strategy pattern - the appropriate operator is chosen based on input.

- Trade-offs are made in favor of speed

- Real time calculations of dependent cell values when cell value is changed. Cells implements Observer pattern to faciliate real time updation.

- As a result there is no separate "EvaluateSheet" or similar method that needs to be called. Entering values in the cell are sufficient to trigger fast recalculations of only the affected cells

- Cyclic dependencies are calculated in real time during "entry" of the values (read input and assign values to cell). Only the affected cells are considered for cyclic dependency checks. 

- The only time entire height x width of the matrix is scanned is during output. The whole matrix is never scanned throghout the execution. Only affected parts are dealt with

- Operators are loaded with reflection and only one time. There was no DI libraries that can be referenced (as per requirement) and hence the decision. 



###Assumptions:

- Empty cells would be displayed as "-". If insufficient entries are there for the spreadsheet, the rest of the cells will be considered empty. No error will be thrown.

- Support Unary operators (++, --) (Extras)

- Support negative numbers (Extras)

- No commandline arguments are supported because it was not required as per the requirments. Instead stdin ('cat' pipe) is read for input

- Strings are not supported as valid inputs. All inputs are assumed to be integers - as per requirement.

- Data type for calculations is double - as per requirement

- The application terminates when cyclic dependency is discovered. It exits with a negative return code (as per requirement).


###Intended - but Omitted Features:

- Configuration file is omitted. Because the requirement was clear enough and configuration would complicate the code for very little gain.

- Multiple strategies were initially thought of for handling Cyclic Dependencies. Implemented few Strategy classes for that. But removed it was not required at this point


