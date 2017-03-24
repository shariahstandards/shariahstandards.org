import { Component, OnInit ,Input} from '@angular/core';
import { PieChartSector} from './pie-chart-sector.interface'
import { PieChartDataItem} from './pie-chart-data-item.interface'
@Component({
  selector: 'pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  constructor() { }

  @Input('size') 
  size:number
  @Input('data')
  data:PieChartDataItem[]

  ngOnInit() {
  }
  // some parts from https://danielpataki.com/svg-pie-chart-javascript/

  calculateSectors():PieChartSector[]{

  		
		var data = {
			size:this.size,
			sectors:this.data
		};
	    if(this.data.some(x=>isNaN(x.percentage)))
  		{
  			data.sectors=[{
  				colour:'#AAA',
  				label:'no data',
  				percentage:100
  			}]
  		}

  		var sectors = [];
	    var l = data.size / 2
	    var a = 0 // Angle
	    var aRad = 0 // Angle in Rad
	    var z = 0 // Size z
	    var x = 0 // Side x
	    var y = 0 // Side y
	    var X = 0 // SVG X coordinate
	    var Y = 0 // SVG Y coordinate
	    var R = 0 // Rotation
	    var aCalc,arcSweep;

	    data.sectors.map( function(item, key ) {
	        aRad = (item.percentage/100.0) * 2 * Math.PI;
	        y=l*Math.sin(aRad);
	        x=l*Math.cos(aRad);
            X = l + x;
            var largeArc=0;
            var arcSweep=1;
	        if( aRad > Math.PI ) {
	         	largeArc=1
	        }
	        Y = l + y;
	        sectors.push({
	            percentage: (item.percentage/100),
	            label: item.label,
	            color: item.colour,
	            arcSweep: arcSweep,
	            largeArc:largeArc,
	            L: l,
	            X: X,
	            Y: Y,
	            R: R
	        });

	        R = R + aRad*180/Math.PI;
	    })

    	return sectors
	}
}
