import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';

@Component({
  selector: 'app-raid',
  templateUrl: './raid.component.html',
  styleUrls: ['./raid.component.css']
})
export class RaidComponent implements OnInit {

  constructor(private api : InternalAPIService ) { }

  ngOnInit(): void {
  }

  raid : any; //store raid {opponent profile Id : situation}
  raidOpponentID : number = -1;

  raidProcess(){

    this.api.Raid().subscribe((res) => {this.raid = res; this.raidOpponentID=this.raid['Item1'] ; console.log(this.raidOpponentID)})
    
  
  }

  raidRes : any[] = [];
  raidResult(){
    this.api.RaidResult(this.raidOpponentID).subscribe( (res) => {this.raidRes = res; console.log(this.raidRes)})
  }
}
