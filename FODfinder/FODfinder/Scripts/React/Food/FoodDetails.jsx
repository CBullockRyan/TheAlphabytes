﻿class FoodDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            details: JSON.parse(this.props.details)
        };
        this.handleclick.bind(this);
    }

    GetFoodNutrientValue(key) {
        if (key != undefined) {
            return key.value;
        }
        return "";
    }

    GetIngredientListHelper(list) {
        var ingredientsList = "";
        for (var i = 0; i < list.length; i++) {
            ingredientsList += list[i][0].Name;
            if (list[i].length > 1) {
                ingredientsList += " (";
                for (var j = 1; j < list[i].length; j++) {
                    if (j == list[i].length - 1) {
                        ingredientsList += list[i][j].Name
                    }
                    else {
                        ingredientsList += list[i][j].Name + ", ";
                    }
                }
                if (i != list.length - 1) {
                    ingredientsList += "), ";
                }
                else {
                    ingredientsList += ")";
                }
            }
            else if (i != list.length - 1) {
                ingredientsList += ", ";
            }
        }
        return ingredientsList;
    }

    GetIngredientList() {
        var primaryIngredients = this.GetIngredientListHelper(this.state.details.PrimaryIngredients);
        var secondaryIngredients = this.GetIngredientListHelper(this.state.details.SecondaryIngredients);
        if (secondaryIngredients != "") {
            return primaryIngredients.concat(", contains 2% or less of: ", secondaryIngredients, ".");
        }
        return primaryIngredients.concat(".");
    }

    

/*(details.PrimaryIngredients.map(i => i.map(j => j.Name).join(", ")).join(", ")).concat("contains 2% or less of: ", details.SecondaryIngredients.map(i => i.map(j => j.Name).join(", ")).join(", "), "."),*/

    componentDidMount() {
        var { details } = this.state;
        var labelNutrients = JSON.parse(details.LabelNutrients);

        $('#nutritionLabel').nutritionLabel({
            allowCustomWidth: true,
            allowNoBorder: true,
            showServingUnitQuantity: true,
            itemName: details.Description,
            ingredientList: this.GetIngredientList(),
            showCalories: labelNutrients.calories != undefined,
            showFatCalories: false,
            showTotalFat: labelNutrients.fat != undefined,
            showSatFat: labelNutrients.saturatedFat != undefined,
            showTransFat: labelNutrients.transFat != undefined,
            showPolyFat: labelNutrients.polyFat != undefined,
            showMonoFat: labelNutrients.monoFat != undefined,
            showCholesterol: labelNutrients.cholesterol != undefined,
            showSodium: labelNutrients.sodium != undefined,
            showPotassium_2018: labelNutrients.potassium != undefined,
            showTotalCarb: labelNutrients.carbohydrates != undefined,
            showFibers: labelNutrients.fiber != undefined,
            showSugars: labelNutrients.sugars != undefined,
            showAddedSugars: false,
            showProteins: labelNutrients.protein != undefined,
            showVitaminA: labelNutrients.vitaminA != undefined,
            showVitaminC: labelNutrients.vitaminC != undefined,
            showVitaminD: labelNutrients.vitaminD != undefined,
            showCalcium: labelNutrients.calcium != undefined,
            showIron: labelNutrients.iron != undefined,
            showCaffeine: labelNutrients.caffeine != undefined,
            valueCalories: this.GetFoodNutrientValue(labelNutrients.calories),
            valueTotalFat: this.GetFoodNutrientValue(labelNutrients.fat),
            valueSatFat: this.GetFoodNutrientValue(labelNutrients.saturatedFat),
            valueTransFat: this.GetFoodNutrientValue(labelNutrients.transFat),
            valuePolyFat: this.GetFoodNutrientValue(labelNutrients.polyFat),
            valueMonoFat: this.GetFoodNutrientValue(labelNutrients.monoFat),
            valueCholesterol: this.GetFoodNutrientValue(labelNutrients.cholesterol),
            valueSodium: this.GetFoodNutrientValue(labelNutrients.sodium),
            valueTotalCarb: this.GetFoodNutrientValue(labelNutrients.carbohydrates),
            valueFibers: this.GetFoodNutrientValue(labelNutrients.fiber),
            valueSugars: this.GetFoodNutrientValue(labelNutrients.sugars),
            valueProteins: this.GetFoodNutrientValue(labelNutrients.protein),
            valueVitaminA: this.GetFoodNutrientValue(labelNutrients.vitaminA),
            valueVitaminC: this.GetFoodNutrientValue(labelNutrients.vitaminC),
            valueVitaminD: this.GetFoodNutrientValue(labelNutrients.vitaminD),
            valuePotassium_2018: this.GetFoodNutrientValue(labelNutrients.potassium),
            valueCalcium: this.GetFoodNutrientValue(labelNutrients.calcium),
            valueIron: this.GetFoodNutrientValue(labelNutrients.iron),
            valueCaffeine: this.GetFoodNutrientValue(labelNutrients.caffeine),
            valueServingSizeUnit: details.ServingSizeUnit,
            valueServingUnitQuantity: details.ServingSize,
            showLegacyVersion: false
        });
    }

    async handleclick() {
        var id = parseInt(this.state.details.FdcId);
        var brand = this.state.details.BrandOwner;
        var desc = this.state.details.Description;
        var barcode = this.state.details.UPC;
        var saveFood = await axios.post(`/SavedFoods/Create`, { usdaFoodID: id, brandOwner: brand, description: desc, upc: barcode });
        var result = saveFood.data;
        var message = result.message;
        if (result.redirect == true) {
            window.location.replace("/Account/Login?ReturnUrl=%2ffood%2fdetails%2f" + id);
        } else {
            alert(message);
        }
        
        window.console.log(message);
    }

    render() {
        var { details } = this.state;
        
        return (
            <div className="pt-4">
                <div className="card bg-secondary text-gray shadow">
                    <div className="card-header">
                        <h2 className="display-4 font-weight-normal text-capitalize">{details.Description.toLowerCase()}</h2>
                        <h3 className="font-weight-light">{details.BrandOwner}</h3>
                        <button type="button" onClick={() => { this.handleclick() }} className="btn btn-primary text-white">Save Food</button>
                    </div>
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-6 d-inline-flex flex-column justify-content-start align-items-start">
                                <p className="text-lowercase">
                                    <span className="font-weight-bold text-capitalize">Ingredients:&nbsp;</span>
                                    {
                                        details.PrimaryIngredients.map((j, jindex) => j.map((i, index) =>
                                            <span key={index}>
                                                {index == 1 ? "(" : ""}
                                                <span key={index} className={"p2" + (i.IsFodmap ? " bg-danger-50 text-white rounded px-1" : "")}>
                                                    {i.Name}
                                                </span>
                                                {index != (j.length - 1) ? "" : j.length > 1 ? ")" : ""}
                                                {jindex == details.PrimaryIngredients.length - 1 ? (details.SecondaryIngredients.length != 0 ? ", " : " ") : j.length > 1 ? (index == 0 ? " " : ", ") : ", "}
                                            </span>))
                                    }
                                    {
                                        details.SecondaryIngredients.length > 0 &&
                                        <span className="font-weight-bold">contains 2% or less of:&nbsp;</span>
                                    }
                                    {
                                        details.SecondaryIngredients.map((j, jindex) => j.map((i, index) =>
                                            <span key={index}>
                                                {index == 1 ? "(" : ""}
                                                <span key={index} className={"p2" + (i.IsFodmap ? " bg-danger-50 text-white rounded px-1" : "")}>
                                                    {i.Name}
                                                </span>
                                                {index != (j.length - 1) ? "" : j.length > 1 ? ")" : ""}
                                                {jindex == details.SecondaryIngredients.length - 1 ? " " : j.length > 1 ? (index == 0 ? " " : ", ") : ", "}
                                            </span>))
                                    }
                                </p>
                                {/* <div> {
                                    details.SecondaryIngredients.length > 0
                                        ? <p className="text-lowercase">
                                            <span className="font-weight-bold text-capitalize">contains 2% or less of:&nbsp;</span>
                                            {
                                            details.SecondaryIngredients.map((j, jindex) => j.map((i, index) =>
                                                <span key={index}>
                                                    {index == 1 ? "(" : ""}
                                                    <span key={index} className={"p2" + (i.IsFodmap ? " bg-danger-50 text-white rounded" : "")}>
                                                        {i.Name}
                                                    </span>
                                                    {index != (j.length - 1) ? "" : j.length > 1 ? ")" : ""}
                                                    {jindex == details.SecondaryIngredients.length + j.length - 1 ? "." : j.length > 1 ? (index == 0 ? " " : ", ") : ", "}
                                                </span>))
                                            }
                                        </p>
                                        : null
                                    }
                                </div>*/}
                                <p className="d-inline-block"><span className="font-weight-bold">UPC:</span> {details.UPC}</p>
                                <p><span className="font-weight-bold">Serving Size:</span> {details.ServingSize}{details.ServingSizeUnit}</p>
                            </div>
                            <div className="col-md-6">
                                <div className="shadow rounded bg-gray p-4">
                                    <div id="nutritionLabel">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}