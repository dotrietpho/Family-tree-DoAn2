import {
  Component,
  Output,
  EventEmitter,
  APP_INITIALIZER
} from "@angular/core";
import { node } from "./shared/models/node";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { TreeService } from "./shared/tree.service";

@Component({
  selector: "app-root",
  template: `
    <div class="row">
      <div class="col-sm-2">
        <app-input-section #inputSection> </app-input-section>
      </div>
      <div class="col-sm-10">
        <app-family-tree
          (onLeafSelected)="inputSection.dataTran($event)"
        ></app-family-tree>
      </div>
    </div>
  `,
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  ngOnInit() {}
}
