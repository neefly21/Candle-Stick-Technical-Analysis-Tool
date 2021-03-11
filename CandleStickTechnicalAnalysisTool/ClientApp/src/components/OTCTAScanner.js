import React, { Component } from 'react';
import Chart from "react-google-charts";
import moment from 'moment';
import constants from '../constants.txt';

export class OTCTAScanner extends Component {

    constructor(props) {
        super(props);
        this.state = { candleStickData: [[]], loading: true, tempTicker: "AAPL", patternOptions: [], selectedPattern: ""};

        this.handleTickerInputChange = this.handleTickerInputChange.bind(this);
        this.handleDropDownOnChange = this.handleDropDownOnChange.bind(this);
        this.debounce = this.debounce.bind(this);
        this.getCandleStickData(this.state.tempTicker);
        this.getPatternListData();
    }

    handleDropDownOnChange(dropDown)
    {
        this.setState({selectedPattern: dropDown});

        $.ajax({
            method: "POST",
            url: "/checkTickerForPattern",
            data: { "patternToScanFor": dropDown }
          })
        .done(function( msg ) {
            alert( "Data Saved: " + msg );
        });
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

        var response = await fetch("https://localhost:44322/getCandleStickChartByTicker/" + ticker);
        const data = await response.json();

        var data2 = [
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

            data2.push(new Object([new Date(tempCandle[0]), parseFloat(tempCandle[4]), parseFloat(tempCandle[2]), parseFloat(tempCandle[5]), parseFloat(tempCandle[3])]));
        }

        this.setState({ candleStickData: data2, loading: false });
    }

    renderCandleStickChart() {

        var candleStickData = this.state.candleStickData;

        return (
            <Chart
                width={'100%'}
                height={350}
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
                                range: { start: new Date(2020, 1, 9), end: new Date(2021, 3, 10) },
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

    render() {

        return (
            <div>
                
                <select select={this.state.selectedPattern} onChange={(e) => this.handleDropDownOnChange(e.target.value)}>
                    {this.renderPatternOptionsToScanFor()}
                </select>
                
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
