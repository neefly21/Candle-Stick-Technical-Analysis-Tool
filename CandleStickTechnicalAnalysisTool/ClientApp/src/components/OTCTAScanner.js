import React, { Component } from 'react';
import Chart from "react-google-charts";
import moment from 'moment';
import constants from '../constants.txt';
import * as $ from 'jquery'


export class OTCTAScanner extends Component {

    constructor(props) {
        super(props);
        this.state = { candleStickData: [[]], loading: true, tempTicker: "AAPL", patternOptions: [], selectedPattern: "", scanChartingData: [], minimumFrequency: 15};

        this.handleTickerInputChange = this.handleTickerInputChange.bind(this);
        this.handleDropDownOnChange = this.handleDropDownOnChange.bind(this);
        this.debounce = this.debounce.bind(this);
        this.postPatternRequest = this.postPatternRequest.bind(this);
        this.formatCandleStickDataForGoogleChart = this.formatCandleStickDataForGoogleChart.bind(this);
        this.handleMinimumFrequencyChange = this.handleMinimumFrequencyChange.bind(this);
        this.getCandleStickData(this.state.tempTicker);
        this.getPatternListData();
    }

    handleDropDownOnChange(dropDown)
    {
        this.setState({selectedPattern: dropDown});
    }

    async getPatternListData()
    {
        var urlForRawOptionsList = "https://localhost:44322/getListOfPatterns";
        var response = await fetch(urlForRawOptionsList);
        const data = await response.json();
        var selectMenuData = [];

        for (const [key, value] of Object.entries(data))
            if(value !== null || value !== undefined)
                selectMenuData.push(value);

        this.setState({patternOptions: selectMenuData});
    }

    async getCandleStickData(ticker) {

        var response = await fetch("http://localhost:44322/getCandleStickChartByTicker/" + ticker);
        const data = await response.json();
        var data2 = this.formatCandleStickDataForGoogleChart(data);

        this.setState({ candleStickData: data2, loading: false });
    }

    formatCandleStickDataForGoogleChart(data)
    {
        var formattedChartData = [
            ['date', 'low', 'open', 'close', 'high'],
        ];

        for (const [key, value] of Object.entries(data)) {
            var tempCandle = [];

            for (const [key2, value2] of Object.entries(value)) {
                tempCandle.push(value2);
            }

            if ((parseFloat(tempCandle[4]) === null || parseFloat(tempCandle[4]) === undefined) ||
                (parseFloat(tempCandle[2]) === null || parseFloat(tempCandle[2]) === undefined) ||
                (parseFloat(tempCandle[5]) === null || parseFloat(tempCandle[5]) === undefined) ||
                (parseFloat(tempCandle[3]) === null || parseFloat(tempCandle[3]) === undefined))
                continue;

            formattedChartData.push(new Object([new Date(tempCandle[0]), parseFloat(tempCandle[4]), parseFloat(tempCandle[2]), parseFloat(tempCandle[5]), parseFloat(tempCandle[3])]));
        }

        return formattedChartData;
    }

    renderCandleStickChart(candleStickData) {
        return (
            <Chart
                width={'100%'}
                height={375}
                chartType="CandlestickChart"
                loader={<div>Loading Chart</div>}
                data={candleStickData}
                options={{
                    legend: 'none',
                    bar: { groupWidth: '75%' }, // Remove space between bars.
                    candlestick: {
                        fallingColor: { strokeWidth: 0, fill: '#a52714' }, // red
                        risingColor: { strokeWidth: 0, fill: '#0f9d58' }, // green
                    },
                }}
                rootProps={{ 'data-testid': '3' }}
                chartPackages={['corechart', 'controls']}
                controls={[
                    {
                        controlType: 'ChartRangeFilter',
                        options: {
                            filterColumnIndex: 0,
                            ui: {
                                chartType: 'CandlestickChart',
                                chartOptions: {
                                    chartArea: { width: '90%', height: '50%' },
                                    hAxis: { baselineColor: 'none' },
                                },
                            },
                        },
                        controlPosition: 'bottom',
                        controlWrapperParams: {
                            state: {
                                range: { start: new Date(2020, 1, 9), end: new Date(2021, 3, 12) },
                            },
                        },
                    }
                ]}
            />
        );
    }

    handleTickerInputChange(ticker)
    {
        this.setState({ tempTicker: ticker });
        this.debouncedGetNewCandleStickData(ticker);
    }

    debouncedGetNewCandleStickData = this.debounce((text) => {
        if(text.length > 2)
            this.getCandleStickData(text);
    }, 250);

    handleMinimumFrequencyChange(minimumFrequency)
    {
        this.setState({ minimumFrequency: minimumFrequency });
    }


    postPatternRequest()
    {
        var that = this;
        var data = {
            "Pattern": this.state.selectedPattern,
            "MinimumFrequency": this.state.minimumFrequency
        }

        $.ajax({
            type: "POST",
            url: "/api/RunPatternDetection",
            data: JSON.stringify(data),
            contentType:"application/json; charset=utf-8",
            dataType:"json",
            success: function( response ) {
                console.log(response);
                that.setState({scanChartingData: response})
              }
          });
    }

    renderCompanyPatternChartingResults(scanResults)
    {
        var data = this.formatCandleStickDataForGoogleChart(scanResults.CandleSticks);
        
        return(
            <tr>
                <td>{scanResults.CompanyRecord.CompanyName}</td>
                <td>{scanResults.CompanyRecord.Symbol}</td>
                <td>
                    <div>
                        {this.renderCandleStickChart(data)}
                    </div>
                </td>
            </tr>
        );
    }

    render() {

        var blah = this.state.scanChartingData;
        var results = [];

        for (const [key, value] of Object.entries(blah)) {
            var temp = this.renderCompanyPatternChartingResults(value)
            results.push(temp);
        }

        return (
            <div>
                <form method="POST">
                        <select select={this.state.selectedPattern} onChange={(e) => this.handleDropDownOnChange(e.target.value)}>
                            {this.renderPatternOptionsToScanFor()}
                        </select>
                        <input type="text" value={this.state.minimumFrequency} onChange={(e) => this.handleMinimumFrequencyChange(e.target.value)} />
                        <input type="button" value="Scan" onClick={this.postPatternRequest}/>
                </form>
                <table>
                    <colgroup>
                        <col span="1" style={{width: "20%"}}/>
                        <col span="1" style={{width: "20%"}}/>
                        <col span="1" style={{width: "60%"}}/>
                    </colgroup>
                <tbody>
                    <tr>
                        <th>Company</th>
                        <th>Ticker</th>
                        <th style={{width: '5rem'}}></th>
                    </tr>
                    {results}
                </tbody>
                </table>
            </div>
        );
    }

    renderTickerInputAndChart()
    {
        return (
            <div>
                <input type="text" value={this.state.tempTicker} placeholder={"Example: AAPL..."} onChange={(e) => this.handleTickerInputChange(e.target.value)}></input>
                <div style={{ display: 'flex', maxWidth: 900 }}>
                    {this.state.tempTicker.length > 2 ? this.renderCandleStickChart() : null}
                </div>
            </div>
        );

    }

    renderPatternOptionsToScanFor()
    {
        let itemList = this.state.patternOptions.map((item,index)=>{
            return <option key={index} value={item.PatternName}>{item.PatternDisplayName}</option>
        })
        
        return itemList;
    }

    debounce(func, wait, immediate) {
        var timeout;
        return function() {
            var context = this, args = arguments;
            var later = function() {
                timeout = null;
                if (!immediate) func.apply(context, args);
            };
            var callNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) func.apply(context, args);
        };
    };

    async postData(url = '', data = {}) {
        // Default options are marked with *
        const response = await fetch(url, {
          method: 'POST', // *GET, POST, PUT, DELETE, etc.
          mode: 'cors', // no-cors, *cors, same-origin
          cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
          credentials: 'same-origin', // include, *same-origin, omit
          headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
          },
          redirect: 'follow', // manual, *follow, error
          referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
          body: JSON.stringify(data) // body data type must match "Content-Type" header
        });
        return response.json(); // parses JSON response into native JavaScript objects
      }
}
