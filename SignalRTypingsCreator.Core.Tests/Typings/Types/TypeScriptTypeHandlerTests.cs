using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalRTypingsCreator.Core.Typings.Types;

namespace SignalRTypingsCreator.Core.Tests.Typings.Types
{
    [TestClass]
    public class TypeScriptTypeHandlerTests
    {
        [TestMethod]
        public void WhenTypeIsString_ReturnsTypeScriptStringType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(String);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("string", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeAliasIsString_ReturnsTypeScriptStringType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(string);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("string", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsInt_ReturnsTypeScriptNumberType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(int);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("number", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsDouble_ReturnsTypeScriptDecimalType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(double);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("decimal", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsFloat_ReturnsTypeScriptDecimalType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(float);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("decimal", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsArrayOfInts_ReturnsTypeScriptNumberArrayType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(int[]);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("number[]", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsListOfInts_ReturnsTypeScriptNumberArrayType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(List<int>);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual("number[]", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsCustomClass_ReturnsClassNameAsType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(DummyClass);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual(nameof(DummyClass), typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsAListOfCustomClasses_ReturnsTypeScriptClassNameArrayType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(IEnumerable<DummyClass>);

            var typeScriptType = typeScriptTypeHandler.GetTypeScriptType(type);

            Assert.AreEqual($"{nameof(DummyClass)}[]", typeScriptType);
        }

        [TestMethod]
        public void WhenTypeIsArray_IsCollection()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(int[]);

            var isCollection = typeScriptTypeHandler.IsCollection(type);

            Assert.IsTrue(isCollection);
        }

        [TestMethod]
        public void WhenTypeIsIEnumerable_IsCollection()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(IEnumerable<int>);

            var isCollection = typeScriptTypeHandler.IsCollection(type);

            Assert.IsTrue(isCollection);
        }

        [TestMethod]
        public void WhenTypeIsClass_NoCollection()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(DummyClass);

            var isCollection = typeScriptTypeHandler.IsCollection(type);

            Assert.IsFalse(isCollection);
        }

        [TestMethod]
        public void WhenTypeIsValueType_NoCollection()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(int);

            var isCollection = typeScriptTypeHandler.IsCollection(type);
            
            Assert.IsFalse(isCollection);
        }

        [TestMethod]
        public void WhenTypeIsClass_UnknownType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(DummyClass);

            var isCollection = typeScriptTypeHandler.IsUnknownType(type);

            Assert.IsTrue(isCollection);
        }

        [TestMethod]
        public void WhenTypeIsValueType_NotUnknownType()
        {
            var typeScriptTypeHandler = new TypeScriptTypeHandler();
            Type type = typeof(int);

            var isCollection = typeScriptTypeHandler.IsUnknownType(type);

            Assert.IsFalse(isCollection);
        }
    }
}
