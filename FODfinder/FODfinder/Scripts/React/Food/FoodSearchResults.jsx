class FoodSearchResults extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            results: JSON.parse(this.props.results),
            currentPage: this.props.currentPage,
            totalPages: this.props.totalPages,
            totalHits: this.props.totalHits,
            query: this.props.query, 
            loading: false
        };

        this.onPageNavigate.bind(this);
        this.onGetPage.bind(this);

        console.log(this.state);
    }
    
    async onGetPage(pNum) {
        var response = await axios.get(`/Food/Get?query=${this.state.query}&page=${pNum}`);
        return response.data;
    }
    
    
    async onPageNavigate(pNum) {
        console.log(pNum);
        this.setState({ loading: true });
        var newResults = await this.onGetPage(pNum);
        this.setState({
            results: newResults.Foods,
            currentPage: newResults.CurrentPage,
            totalPages: newResults.TotalPages,
            totalHits: newResults.TotalHits,
            loading: false
        });
    }

    render() {
        var { results, currentPage, totalPages, totalHits, loading } = this.state;

        var pageNumbers = Array.apply(null, Array(totalPages)).map((_, i) => { return i; });

        return (
            <div className="container p-0 p-lg-3">
                <div className="row bg-primary rounded my-4">
                    <div className="col-md-4  p-4 d-inline-flex flex-column align-items-center justify-content-center">
                        <p className="bg-secondary rounded p-2 text-white shadow"><span className="font-weight-bold">Current Page</span>: {currentPage}</p>
                    </div>
                    <div className="col-md-4 p-4 d-inline-flex flex-column align-items-center justify-content-center">
                        <p className="bg-secondary rounded p-2 text-white shadow"><span className="font-weight-bold">Total Pages</span>: {totalPages}</p>
                    </div>
                    <div className="col-md-4 p-4 d-inline-flex flex-column align-items-center justify-content-center">
                        <p className="bg-secondary rounded p-2 text-white shadow"><span className="font-weight-bold">Total Hits</span>: {totalHits}</p>
                    </div>
                </div>
                <div className={loading ? "LoadingWrapper LoadingBlur" : "LoadingWrapper"}>
                    <FoodSearchResultItems results={results} />
                </div>
                <ul className="pagination pagination-lg justify-content-center flex-wrap">
                    {
                        pageNumbers.map((p, i) => {
                            return (<li key={"page-"+(i+1)} className={"page-item" + (i + 1 == currentPage ? " active" : "")}><a onClick={() => { this.onPageNavigate(i+1) }} className="page-link">{i + 1}</a></li>)
                        })
                    }
                </ul>
            </div>
        )
    }
}