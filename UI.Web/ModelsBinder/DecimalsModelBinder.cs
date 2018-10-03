using System;
using System.Globalization;
using System.Web.Mvc;

namespace UI.Web.ModelsBinder
{

    public class DecimalModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object result = null;

            string modelName = bindingContext.ModelName;
            string attemptedValue = bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

            string wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            string alternateSeperator = (wantedSeperator == "," ? "." : ",");

            if (attemptedValue.IndexOf(wantedSeperator) == -1 && attemptedValue.IndexOf(alternateSeperator) != -1)
            {
                attemptedValue = attemptedValue.Replace(alternateSeperator, wantedSeperator);
            }

            try
            {
                if (bindingContext.ModelMetadata.IsNullableValueType && string.IsNullOrWhiteSpace(attemptedValue))
                {
                    return null;
                }

                result = decimal.Parse(attemptedValue, NumberStyles.Any);
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.AddModelError(modelName, e);
            }

            return result;
        }


        //public object BindModel(ControllerContext controllerContext,
        //    ModelBindingContext bindingContext)
        //{
        //    ValueProviderResult valueResult = bindingContext.ValueProvider
        //        .GetValue(bindingContext.ModelName);
        //    ModelState modelState = new ModelState { Value = valueResult };
        //    object actualValue = null;
        //    try
        //    {
        //        actualValue = Convert.ToDecimal(valueResult.AttemptedValue,
        //            CultureInfo.CurrentCulture);
        //    }
        //    catch (FormatException e)
        //    {
        //        modelState.Errors.Add(e);
        //    }

        //    bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
        //    return actualValue;
        //}
    }

}