using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI.Web.Helpers
{
    public static class TextHelpers
    {
        public static MvcHtmlString Saldo(this HtmlHelper helper, decimal saldo)
        {
            var strong = new TagBuilder("strong");

            if (saldo < 0)
            {
                strong.Attributes["style"] = "color: red";
                strong.SetInnerText(String.Format("- {0}", Math.Abs(saldo)));
            }

            if (saldo == 0)
            {
                strong.SetInnerText(saldo.ToString());
            }

            if (saldo > 0)
            {
                strong.Attributes["style"] = "color: green";
                strong.SetInnerText(String.Format("+ {0}", saldo));
            }

            return MvcHtmlString.Create(strong.ToString());
        }

        public static MvcHtmlString MovimientoStock(this HtmlHelper helper, int stock)
        {
            var strong = new TagBuilder("strong");

            if (stock < 0)
            {
                strong.Attributes["style"] = "color: red";
                strong.SetInnerText(String.Format("- {0}", Math.Abs(stock)));
            }

            if (stock == 0)
            {
                strong.SetInnerText(stock.ToString());
            }

            if (stock > 0)
            {
                strong.Attributes["style"] = "color: green";
                strong.SetInnerText(String.Format("+ {0}", stock));
            }

            return MvcHtmlString.Create(strong.ToString());
        }

        public static MvcHtmlString FechaAnioMesDia(this HtmlHelper helper, string fecha)
        {
            DateTime fechaOut = new DateTime();
            string newFecha = "";
            if (DateTime.TryParse(fecha, out fechaOut))
            {
                newFecha = fecha.Substring(8) + "/" + fecha.Substring(6, 2) + "/" + fecha.Substring(0, 2);
            }

            return MvcHtmlString.Create(newFecha.ToString());
        }

        public static MvcHtmlString Cantidad(this HtmlHelper helper, decimal cantidad)
        {
            var TotalMostrar = "";
            if (cantidad.ToString().Contains(","))
            {
                if (cantidad.ToString().Split(',')[0] != "0") { TotalMostrar = cantidad.ToString().Split(',')[0]; }
                if (cantidad.ToString().Split(',')[1] == "50") { TotalMostrar = TotalMostrar + " 1/2"; }
            }

            return MvcHtmlString.Create(TotalMostrar.ToString());
        }

        public static MvcHtmlString Total(this HtmlHelper helper, decimal total)
        {
            var TotalMostrar = "";
            if (total.ToString().Contains(","))
            {
                int index = total.ToString().IndexOf(",");
                TotalMostrar = total.ToString().Substring(0, index + 3);
            }

            return MvcHtmlString.Create(TotalMostrar.ToString());
        }

        public static MvcHtmlString Decimal(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
        {
            var builder = new TagBuilder("input");

            RouteValueDictionary customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            foreach (KeyValuePair<string, object> customAttribute in customAttributes)
            {
                builder.MergeAttribute(customAttribute.Key.ToString(), customAttribute.Value.ToString());
            }

            builder.Attributes["type"] = "number";
            builder.Attributes["pattern"] = "(^\\d+(\\.|\\,)\\d{2}$";
            builder.Attributes["required"] = "required";
            builder.Attributes["name"] = name;
            if (value.ToString().Contains(".") || value.ToString().Contains(","))
            {
                var valor = value.Substring(0, value.IndexOf('.')) + value.Substring(value.IndexOf('.'), 3);
                builder.Attributes["value"] = valor;
            }
            else
            {
                builder.Attributes["value"] = value;
            }
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Decimal(this HtmlHelper htmlHelper, string name, string value)
        {
            var builder = new TagBuilder("input");

            builder.Attributes["type"] = "number";
            builder.Attributes["pattern"] = "(^\\d+(\\.|\\,)\\d{2}$";
            builder.Attributes["required"] = "required";
            builder.Attributes["name"] = name;
            builder.Attributes["value"] = value;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString DecimalFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, decimal value, object htmlAttributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string valorDecimal = value.ToString();
            try
            {
                valorDecimal = value.ToString().Replace(",", ".");
            }
            catch
            { }

            return Decimal(htmlHelper, name, valorDecimal, htmlAttributes);
        }

        public static MvcHtmlString DisplayStock(this HtmlHelper helper, int total, int stockMinimo)
        {
            var strong = new TagBuilder("strong");

            if (total <= stockMinimo)
            {
                strong.Attributes["style"] = "color: red";
                strong.SetInnerText(String.Format("{0}", total));
            }

            if (total > stockMinimo && total <= stockMinimo * 1.2)
            {
                strong.Attributes["style"] = "color: orange";
                strong.SetInnerText(String.Format("{0}", total));
            }

            if (total > stockMinimo * 1.2)
            {
                strong.Attributes["style"] = "color: green";
                strong.SetInnerText(String.Format("{0}", total));
            }

            return MvcHtmlString.Create(strong.ToString());
        }
    }
}