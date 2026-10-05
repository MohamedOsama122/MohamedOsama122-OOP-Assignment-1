# Critique: Procedural C++ Order System

The supplied C++ program works, but its structure makes ordinary changes risky because data and rules are separated from the things they describe.

## 1. Global mutable state

All customers, products, orders, stock values, and counters are global arrays or global variables. Any free function can change any value, so there is no clear owner for rules such as "a paid order cannot change" or "stock cannot become negative." A future function can accidentally bypass a check and leave the program in an invalid state. Global state also makes the program difficult to test independently or run in more than one context.

## 2. Parallel arrays do not model the domain

A single customer is spread across `customerIds`, `customerNames`, `customerEmails`, `customerCities`, and `customerIsVip`. Products, orders, and order lines are represented the same way. The common array index is an undocumented relationship: inserting, removing, or updating one array incorrectly breaks the whole entity. A `Customer`, `Product`, `Order`, and `OrderLine` class groups related data and behaviour into one meaningful unit.

## 3. Order relationships are stored as fragile integer indexes

Orders store a customer-array index and lines store product-array indexes rather than referring to actual domain objects. This makes the code depend on internal storage positions and requires every function to translate IDs to indexes. If storage changes, relationships can silently point to the wrong record. Object references make the customer and product behind an order line explicit.

## 4. Fixed-size storage creates artificial failures

Constants such as `MAX_CUSTOMERS`, `MAX_PRODUCTS`, `MAX_ORDERS`, and `MAX_LINES_PER_ORDER` limit otherwise valid use cases. The program rejects records simply because an array is full, and the limits are duplicated implementation details rather than business requirements. A `List<T>` grows as needed and lets the application enforce only rules that actually belong to the domain.

## 5. Business rules are scattered across unrelated free functions

Stock reduction lives in `addLineToOrder`, the VIP discount lives in `calculateOrderTotal`, and payment state is changed in `markOrderPaid`. The functions must know the layout of every global array and can easily drift apart. For example, a new way to add a line might forget to reduce stock. Putting `ReserveStock` on `Product`, `AddLine` and `Pay` on `Order`, and discount logic on `Customer` makes each rule discoverable and reusable.

## 6. Validation and error handling are tied to console output

Functions print errors and return sentinel values such as `-1`. This mixes user-interface work with domain work and makes failures easy for callers to ignore. The redesigned application uses domain operations that throw clear exceptions; the console menu catches those exceptions and presents the message to the user.

## 7. Repetitive searching and display logic obscure intent

Several nearly identical loops search arrays by ID, while order totals and printing repeatedly reconstruct the data relationships by hand. This repetition is fertile ground for off-by-one errors and inconsistent behaviour. An application service can own ID lookup, while entities compute their own totals and expose their own data for display.
