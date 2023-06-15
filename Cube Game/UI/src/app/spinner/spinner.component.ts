import { Component, ViewEncapsulation } from '@angular/core';
import { LoaderService } from '../../Services/loader.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css'],
  encapsulation: ViewEncapsulation.ShadowDom

})
export class SpinnerComponent {
  constructor(public loader: LoaderService) { }

}
