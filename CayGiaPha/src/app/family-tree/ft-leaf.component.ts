import { Component, EventEmitter, Input, Output } from "@angular/core";
import { node } from "../shared/models/node";
import { TreeService } from "../shared/tree.service";

@Component({
  selector: "ft-leaf",
  template: `
    <div>
      <span
        *ngFor="let node of child.persons || async"
        class="node"
        [ngClass]="node.relationship ? node.relationship + '-leaf' : ''"
        (click)="_leafSelected(node)"
        [class]="node.personGender"
        ><img src="{{ hostURL }}{{ node.personImg }}" /><br />{{
          node.personName
        }}</span
      >
    </div>
    <ul *ngIf="child.childs && child.childs.length > 0">
      <li
        *ngFor="let row of child.childs || async"
        [ngStyle]="{ width: child.childs.length === 1 ? '100%' : 'auto' }"
      >
        <ft-leaf
          (onLeafSelected)="_leafSelected($event)"
          [child]="row"
        ></ft-leaf>
      </li>
    </ul>
  `,
  styleUrls: ["./family-tree.component.scss"]
})
export class FtLeafComponent {
  @Input() child: node;
  @Output() onLeafSelected: EventEmitter<Node> = new EventEmitter();

  hostURL: string = "";

  constructor(private treeService: TreeService) {
    this.hostURL = treeService.hostURL;
  }

  _leafSelected(_leaf) {
    this.onLeafSelected.emit(_leaf);
  }
}
