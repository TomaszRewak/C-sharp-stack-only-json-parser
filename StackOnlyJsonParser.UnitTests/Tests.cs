using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.Json;

namespace StackOnlyJsonParser.UnitTests
{
	[TestClass]
	public sealed partial class Tests
	{
		[TestMethod]
		public void DeserializesData()
		{
			var data = Encode(@"{ ""Double"": 10.5, ""Int"": 3, ""String"": ""Text"" }");

			var model = new StackOnlyType(data);

			Assert.AreEqual(10.5, model.Double);
			Assert.AreEqual(3, model.Int);
			Assert.AreEqual("Text", model.String);
		}

		[TestMethod]
		public void NotSetDataHaveDefaultValue()
		{
			var data = Encode(@"{ ""Double"": 10.5, ""String"": ""Text"" }");

			var model = new StackOnlyType(data);

			Assert.AreEqual(default, model.Int);
		}

		[TestMethod]
		public void FieldsCanBeProvidedInAnyOrder()
		{
			var data = Encode(@"{ ""String"": ""Text"", ""Int"": 3, ""Double"": 10.5 }");

			var model = new StackOnlyType(data);

			Assert.AreEqual(10.5, model.Double);
			Assert.AreEqual(3, model.Int);
			Assert.AreEqual("Text", model.String);
		}

		[TestMethod]
		public void DeserializedObjectIsMarkedAsPresent()
		{
			var data = Encode(@"{}");

			var model = new StackOnlyType(data);

			Assert.IsTrue(model.HasValue);
		}

		[TestMethod]
		public void NullObjectDoesntHaveValue()
		{
			var data = Encode(@"null");

			var model = new StackOnlyType(data);

			Assert.IsFalse(model.HasValue);
		}

		[TestMethod]
		public void DeserializerProgressesToTheNextCharacterIfFirstIsInvalid()
		{
			var reader = Read(@"{ ""A"": { ""Int"": 3 } }");
			reader.Read(); // "{"
			reader.Read(); // "A"

			var model = new StackOnlyType(ref reader);

			Assert.IsTrue(model.HasValue);
			Assert.AreEqual(3, model.Int);
		}

		[TestMethod]
		public void DeserializerDoesNotProgressToTheNextCharacterIfFirstIsValid()
		{
			var reader = Read(@"{ ""A"": { ""Int"": 3 } }");
			reader.Read(); // "{"
			reader.Read(); // "A"
			reader.Read(); // "{"

			var model = new StackOnlyType(ref reader);

			Assert.IsTrue(model.HasValue);
			Assert.AreEqual(3, model.Int);
		}

		[TestMethod, ExpectedException(typeof(JsonException))]
		public void DeserializerDoesNotSkipTwoSymbolsIfBothAreInvalid()
		{
			var reader = Read(@"{ ""A"": 1, ""B"": { ""Int"": 3 } }");
			reader.Read(); // "{"
			reader.Read(); // "A"
			reader.Read(); // "1"

			new StackOnlyType(ref reader);
		}

		[TestMethod]
		public void DeserializerStopsAtLastCharacter()
		{
			var reader = Read(@"{ ""Int"": 3 }");

			new StackOnlyType(ref reader);

			Assert.AreEqual(JsonTokenType.EndObject, reader.TokenType);
		}

		[TestMethod]
		public void UnknownFieldsAreSkipped()
		{
			var data = Encode(@"{ ""Double"": 10.5, ""Unknown"" : 24, ""Int"": 3 }");

			var model = new StackOnlyType(data);

			Assert.AreEqual(10.5, model.Double);
			Assert.AreEqual(3, model.Int);
		}

		[TestMethod]
		public void UnknownFieldsOfCustomTypesAreSkipped()
		{
			var data = Encode(@"{ ""Double"": 10.5, ""Unknown"": { ""Double"": 25 }, ""Int"": 3 }");

			var model = new StackOnlyType(data);

			Assert.AreEqual(10.5, model.Double);
			Assert.AreEqual(3, model.Int);
		}

		[TestMethod, ExpectedException(typeof(InvalidOperationException))]
		public void NullValueIsNotAcceptedForNotNullableFields()
		{
			var data = Encode(@"{ ""Double"": null, ""Int"": 3 }");

			new StackOnlyType(data);
		}

		[TestMethod]
		public void NullableFieldsHaveNullValueWhenNotSet()
		{
			var data = Encode(@"{ }");

			var model = new NullableStackOnlyType(data);

			Assert.AreEqual(null, model.Int);
			Assert.AreEqual(null, model.Double);
		}

		[TestMethod]
		public void NullableFieldsCanBeSetWithNullValue()
		{
			var data = Encode(@"{ ""Int"": null }");

			var model = new NullableStackOnlyType(data);

			Assert.AreEqual(null, model.Int);
		}

		[TestMethod]
		public void NullableFieldsCanBeSetWithNotNullValue()
		{
			var data = Encode(@"{ ""Int"": 3 }");

			var model = new NullableStackOnlyType(data);

			Assert.AreEqual(3, model.Int);
		}

		[TestMethod]
		public void FieldAttributeWithNoNameDefaultsToThePropertyName()
		{
			var data = Encode(@"{ ""String"": ""Text"" }");

			var model = new StackOnlyTypeWithCustomNames(data);

			Assert.AreEqual("Text", model.String);
		}

		[TestMethod]
		public void FieldNameHastToBeAnExactMatch()
		{
			var data = Encode(@"{ ""string"": ""Text"" }");

			var model = new StackOnlyTypeWithCustomNames(data);

			Assert.AreEqual(null, model.String);
		}

		[TestMethod]
		public void WhenProvidedACustomNameIsUsed()
		{
			var data = Encode(@"{ ""int-value"": 3 }");

			var model = new StackOnlyTypeWithCustomNames(data);

			Assert.AreEqual(3, model.Int);
		}

		[TestMethod]
		public void WhenACustomNameIsProvidedTheDefaultOneNoLongerWorks()
		{
			var data = Encode(@"{ ""Int"": 3 }");

			var model = new StackOnlyTypeWithCustomNames(data);

			Assert.AreEqual(default, model.Int);
		}

		[TestMethod]
		public void AllCustomNamesCanBeUsed()
		{
			var data1 = Encode(@"{ ""double-value"": 3.5 }");
			var data2 = Encode(@"{ ""double_value"": 3.5 }");

			var model1 = new StackOnlyTypeWithCustomNames(data1);
			var model2 = new StackOnlyTypeWithCustomNames(data2);

			Assert.AreEqual(3.5, model1.Double);
			Assert.AreEqual(3.5, model2.Double);
		}

		[TestMethod]
		public void NestedMessagesAreDeserialized()
		{
			var data = Encode(@"{ ""Value1"": { ""Int"": 1 }, ""Value2"": { ""Int"": 2 } }");

			var model = new NestingStackOnlyType(data);

			Assert.AreEqual(1, model.Value1.Int);
			Assert.AreEqual(2, model.Value2.Int);
		}

		[TestMethod]
		public void NotPresentNestedMessagesDontHaveValue()
		{
			var data = Encode(@"{ ""Value2"": { ""Int"": 2 } }");

			var model = new NestingStackOnlyType(data);

			Assert.IsFalse(model.Value1.HasValue);
		}

		[TestMethod]
		public void EmptyTypeCanBeDeserialized()
		{
			var data = Encode(@"{ }");

			var model = new EmptyStackOnlyType(data);

			Assert.IsTrue(model.HasValue);
		}

		[TestMethod]
		public void NullArrayHasNoValueAndNoElements()
		{
			var data = Encode(@"null");

			var model = new StackOnlyIntArray(data);

			Assert.IsFalse(model.HasValue);
			Assert.IsFalse(model.Any());
		}

		[TestMethod]
		public void NullArrayCanBeIterated()
		{
			var data = Encode(@"null");
			var iterations = 0;

			var model = new StackOnlyIntArray(data);
			foreach (var _ in model)
				iterations++;

			Assert.AreEqual(0, iterations);
		}

		[TestMethod]
		public void EmptyArrayHasValueButNoElements()
		{
			var data = Encode(@"[]");

			var model = new StackOnlyIntArray(data);

			Assert.IsTrue(model.HasValue);
			Assert.IsFalse(model.Any());
		}

		[TestMethod]
		public void DeserializedArrayCanBeIterated()
		{
			var data = Encode(@"[ 1, 2, 3, 4 ]");
			var sum = 0;

			var model = new StackOnlyIntArray(data);
			foreach (var entry in model)
				sum += entry;

			Assert.AreEqual(10, sum);
		}

		[TestMethod]
		public void NullableElementsAreDeserializedWithinAnArray()
		{
			var data = Encode(@"[ 1, null, 2, 3, null, 4 ]");
			var sum = 0;
			var iterations = 0;

			var model = new StackOnlyNullableIntArray(data);
			foreach (var entry in model)
			{
				sum += entry ?? 0;
				iterations++;
			}

			Assert.AreEqual(10, sum);
			Assert.AreEqual(6, iterations);
		}

		[TestMethod]
		public void ArrayOfCustomTypesCanBeDeserialized()
		{
			var data = Encode(@"[ { ""Int"": 1 }, { ""Int"": 2 }, null, { ""Int"": 3 } ]");

			var model = new StackOnlyTypeArray(data);
			var enumerator = model.GetEnumerator();

			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreEqual(1, enumerator.Current.Int);
			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreEqual(2, enumerator.Current.Int);
			Assert.IsTrue(enumerator.MoveNext());
			Assert.IsFalse(enumerator.Current.HasValue);
			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreEqual(3, enumerator.Current.Int);
			Assert.IsFalse(enumerator.MoveNext());
		}

		[TestMethod]
		public void DictionaryOfCustomTypesCanBeDeserialized()
		{
			var data = Encode(@"{ ""A"": { ""Int"": 1 }, ""B"": { ""Int"": 2 } }");

			var model = new StackOnlyDictionary(data);
			var enumerator = model.GetEnumerator();

			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreEqual("A", enumerator.Current.Key);
			Assert.AreEqual(1, enumerator.Current.Value.Int);
			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreEqual("B", enumerator.Current.Key);
			Assert.AreEqual(2, enumerator.Current.Value.Int);
			Assert.IsFalse(enumerator.MoveNext());
		}

		[TestMethod]
		public void ArraysCanBeNested()
		{
			var data = Encode(@"[ [ { ""Int"": 1 }, { ""Int"": 2 } ], [ { ""Int"": 3 } ] ]");
			var output = new StringBuilder();

			var model = new StackOnlyNestedArray(data);

			foreach (var externalEntry in model)
			{
				foreach (var internalEntry in externalEntry)
					output.Append(internalEntry.Int);
				output.Append("|");
			}

			Assert.AreEqual("12|3|", output.ToString());
		}

		[TestMethod]
		public void CustomTypesCanBeUsedAsDictionaryKeys()
		{
			var data = Encode(@"{ ""A"": 1, ""B"": 2 }");

			var model = new StrictlyStackOnlyDictionary(data);
			var enumerator = model.GetEnumerator();

			Assert.IsTrue(enumerator.MoveNext());
			Assert.IsTrue(enumerator.Current.Key.ValueTextEquals("A"));
			Assert.IsTrue(enumerator.MoveNext());
			Assert.IsTrue(enumerator.Current.Key.ValueTextEquals("B"));
			Assert.IsFalse(enumerator.MoveNext());
		}

		[TestMethod]
		public void GetOnlyPropertiesAreNotDeserialized()
		{
			var data = Encode(@"{ ""Int"": 3, ""Double"": 10, ""Multiplied"": 2 }");

			var model = new StackOnlyType(data);

			Assert.AreEqual(30, model.Multiplied);
		}

		[TestMethod]
		public void LazyLoadingAllowsForRecursiveStructures()
		{
			var data = Encode(@"{ ""Id"": 1, ""Internal"": { ""Internal"": { ""Id"": 3 }, ""Id"": 2 } }");

			var model = new RecursiveStackOnlyType(data);

			Assert.IsTrue(model.Internal.HasValue);
			Assert.AreEqual(1, model.Id);
			Assert.AreEqual(2, model.Internal.Load().Id);
			Assert.AreEqual(3, model.Internal.Load().Internal.Load().Id);
			Assert.IsFalse(model.Internal.Load().Internal.Load().Internal.HasValue);
		}
	}
}
