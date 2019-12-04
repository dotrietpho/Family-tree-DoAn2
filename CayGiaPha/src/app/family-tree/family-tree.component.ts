import {
  Component,
  OnInit,
  Input,
  ViewEncapsulation,
  Output,
  EventEmitter
} from "@angular/core";
import { node } from "../shared/models/node";
import { TreeService } from "../shared/tree.service";

@Component({
  selector: "app-family-tree",
  template: `
    <div class="tree">
      <ul>
        <li>
          <div class="top">
            <span
              *ngFor="let node of tree.persons || async"
              class="node"
              [ngClass]="node.relationship ? node.relationship + '-leaf' : ''"
              (click)="_leafSelected(node)"
              [class]="node.personGender"
              ><img src="{{ hostURL }}{{ node.personImg }}" /><br />{{
                node.personName
              }}</span
            >
          </div>
          <ul>
            <li
              *ngFor="let child of tree.childs || async"
              [ngStyle]="{ width: _setWidth(child) ? '100%' : 'auto' }"
            >
              <ft-leaf
                (onLeafSelected)="_leafSelected($event)"
                [child]="child"
              ></ft-leaf>
            </li>
          </ul>
        </li>
      </ul>
    </div>
  `,
  styleUrls: ["./family-tree.component.scss"],
  encapsulation: ViewEncapsulation.None
})
export class FamilyTreeComponent implements OnInit {
  @Output() onLeafSelected: EventEmitter<node> = new EventEmitter();

  tree = {};
  hostURL: string = "";

  constructor(private treeService: TreeService) {
    this.hostURL = treeService.hostURL;
    this.treeService.getTree().subscribe(data => {
      this.tree = data;
      console.log(data);
    });
  }

  OnInit() {}

  //Tra ve person khi click vao person do
  _leafSelected(_leaf) {
    this.onLeafSelected.emit(_leaf);
  }

  //Ham dieu chinh chieu rong cua cac node co nhieu persons (nhieu parent)
  _setWidth(child: node) {
    return !child.persons;
    //&&
    // child.persons[0].relationship === "self" &&
    // child.persons.length < 2
  }

  ngOnInit() {
    console.log(this.tree);
  }
}
