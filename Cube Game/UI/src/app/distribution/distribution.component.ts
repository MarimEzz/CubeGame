import { Component } from '@angular/core';
import { MostPlayedService } from '../../Services/most-played.service';

@Component({
  selector: 'app-distribution',
  templateUrl: './distribution.component.html',
  styleUrls: ['./distribution.component.css']
})
export class DistributionComponent {


  constructor() {
    this.handleScroll = this.handleScroll.bind(this);
  }

  ngOnInit() {
    window.addEventListener('scroll', this.handleScroll);
  }

  ngOnDestroy() {
    window.removeEventListener('scroll', this.handleScroll);
    this.stopCounters();
  }

  handleScroll() {
    const scrollPosition = window.scrollY;
    if (scrollPosition >= 1350) {
      if (!this.countersRunning) {
        this.startCounters();
        this.countersRunning = true;
      }
    } else {
      if (this.countersRunning) {
        this.stopCounters();
        this.countersRunning = false;
      }
    }
  }

  gamecount: number = 0;
  genercount: number = 0;
  publishercount: number = 0;
  gamecountstop: any;
  genercountstop: any;
  publishercountstop: any;
  gamecountReached: boolean = false;
  genercountReached: boolean = false;
  publishercountReached: boolean = false;
  countersRunning: boolean = false;

  startCounters() {
    if (!this.gamecountReached) {
      if (this.gamecount < 50) {
        this.gamecountstop = setInterval(() => {
          if (this.gamecount < 50) {
            this.gamecount++;
          } else {
            clearInterval(this.gamecountstop);
            this.gamecountReached = true;
          }
        }, 20);
      }
    }

    if (!this.genercountReached) {
      if (this.genercount < 70) {
        this.genercountstop = setInterval(() => {
          if (this.genercount < 70) {
            this.genercount++;
          } else {
            clearInterval(this.genercountstop);
            this.genercountReached = true;
          }
        }, 20);
      }
    }

    if (!this.publishercountReached) {
      if (this.publishercount < 120) {
        this.publishercountstop = setInterval(() => {
          if (this.publishercount < 120) {
            this.publishercount++;
          } else {
            clearInterval(this.publishercountstop);
            this.publishercountReached = true;
          }
        }, 20);
      }
    }
  }

  stopCounters() {
    clearInterval(this.gamecountstop);
    clearInterval(this.genercountstop);
    clearInterval(this.publishercountstop);
    this.gamecount = 0;
    this.genercount = 0;
    this.publishercount = 0;
    this.gamecountReached = false;
    this.genercountReached = false;
    this.publishercountReached = false;
  }
}

