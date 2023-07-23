# Strong Typed Enum 

In .NET and C#, the Type-Safe Enum pattern is an alternative approach to representing a set of named constant values. It provides a more robust and expressive way of working with discrete options or states by leveraging the characteristics of classes and objects rather than relying on integers or strings.

The advantages of the Type-Safe Enum pattern include robust typing, extensibility and the ability to define specific behaviors for each enum value. It offers greater flexibility and allows the use of object-oriented principles when working with enum values.

## Description

Mainly the idea of this repository is to simplify and serve as an accelerator to quickly build enums based on the Type-Safe Enum pattern.

## How to use it?

```csharp
[StrongTypedEnum("Red", "Blue")]
public partial class Color { }
```

The **StrongTypedEnum** attribute is used, and it is passed as many parameters as there are fields in the enum. Then you must create a partial class with the name of the enum.

## Contributions

Contributions are highly appreciated! Should you encounter any issues or wish to improve the project, please submit a pull request. To ensure effective collaboration, kindly adhere to our contribution guidelines.

## License

This project is licensed under the MIT License. For more information, please refer to the [LICENSE file](LICENSE).

## Contact

For any questions or comments, feel free to reach out to us via Discord or Twitter.

Enjoy!

