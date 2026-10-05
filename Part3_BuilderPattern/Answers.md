# Task 3.1 - Why a 20-parameter constructor is a problem

A constructor with around twenty parameters is difficult to read at every call site. A reader has to remember the position and meaning of many values, and the compiler cannot distinguish values that share a type. For example, two `decimal` amounts can easily be swapped, or a shipping city can be passed where a billing city belongs. Adding an optional property later changes the constructor signature and forces every caller to be reviewed or rewritten.

The deeper problem is not only its length. The values belong to different concepts: customer identity, a billing address, a shipping address, and order/payment details. Treating all of them as one flat argument list hides those boundaries and makes validation and reuse difficult.

# Task 3.2 - Why the fluent Builder helps

A fluent builder gives each value a descriptive method name, so calls are self-documenting and ordering no longer matters. A builder can collect optional choices naturally and can validate mandatory fields in `Build()` with an immediate, specific exception. This prevents a partly initialized invoice from being created.

# Task 3.3 - Why composed builders are better

`AddressBuilder` has one responsibility: create a complete, valid address. It can validate street, city, country, and postal code without the invoice builder knowing address rules. The same class is reused for both billing and shipping, avoiding duplicated fields, methods, and validation.

`OrderBuilder` owns order and payment values, including monetary validation and total calculation. The final composition builder only coordinates the already-valid groups and the customer/invoice identity. This separation makes independent validation possible and produces a readable call site: billing and shipping are visibly separate address blocks, while order details are visibly an order block.
