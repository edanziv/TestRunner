using System;

//custom attributes to recognize tests, setup and teardown methods
//only methods will be considered as tests, setup or teardown

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class SetupAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class TeardownAttribute : Attribute { }