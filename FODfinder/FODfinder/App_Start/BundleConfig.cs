﻿using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace FODfinder
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/barPlot").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/barPlot.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new BabelBundle("~/bundles/React").Include(
                    "~/Scripts/React/react.development.js",
                    "~/Scripts/React/react-dom.development.js"));

            bundles.Add(new BabelBundle("~/bundles/FoodSearchResults").Include(
                "~/Scripts/React/Food/FoodSearchResultItems.jsx",
                "~/Scripts/React/Food/FoodSearchResults.jsx"));

            bundles.Add(new BabelBundle("~/bundles/FoodDetails").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/chartsloader.js",
                "~/Scripts/nutritionLabel-min.js",
                "~/Scripts/React/Food/FoodDetails.jsx"));

            bundles.Add(new BabelBundle("~/bundles/FoodSearchBundle").Include(
                "~/Scripts/React/Food/FoodSearch.jsx"));

            BundleTable.EnableOptimizations = true;

        }
    }
}
