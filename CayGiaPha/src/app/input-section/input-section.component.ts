import { Component, OnInit, Input, ɵConsole } from "@angular/core";
import { node } from "../shared/models/node";
import { TreeService } from "../shared/tree.service";
import { HttpClient } from "@angular/common/http";
import { Person } from "../shared/models/Person";

@Component({
  selector: "app-input-section",
  templateUrl: "./input-section.component.html",
  styleUrls: ["./input-section.component.css"]
})
export class InputSectionComponent implements OnInit {
  person: Person = new Person();
  newPerson: Person = new Person();
  newPersonPicture: File = null;
  newPersonPictureFormData: FormData = new FormData();
  previewUrl: any = null;
  hostURL = "";
  constructor(private treeService: TreeService) {
    this.hostURL = treeService.hostURL;
  }

  //Thay doi thong tin person dang duoc chon
  _personChange(person) {
    this.treeService.updatePerson(person);
  }

  //Them con xuong duoi person dang duoc chon (person dang duoc chon la father)
  _addChild(person) {
    this.treeService.addChild(
      this.person.personId,
      this.newPerson,
      this.newPersonPictureFormData
    );
  }

  //Them parent len tren person dang duoc chon (person dang duoc chon la child)
  _addParent(person) {
    this.treeService.addParent(
      this.person.personId,
      this.newPerson,
      this.newPersonPictureFormData
    );
  }

  //Them partnen vao cung node voi person dang duoc chon
  _addPartner(person) {
    this.treeService.addPartner(
      this.person.personId,
      this.newPerson,
      this.newPersonPictureFormData
    );
  }

  //Delete person dang duoc chon va con cua person do
  _deletePerson(person) {
    this.treeService.deletePerson(person.personId);
  }

  onFileChanged(event) {
    let selectedFile = event.target.files[0];
    const uploadData = new FormData();
    uploadData.append("file", selectedFile, selectedFile.name);
    this.treeService.uploadFile(this.person.personId, uploadData);
  }

  onFileChanged2(event) {
    this.newPersonPicture = event.target.files[0];
    const uploadData = new FormData();
    uploadData.append(
      "file",
      this.newPersonPicture,
      this.newPersonPicture.name
    );
    this.newPersonPictureFormData = uploadData;
    this.preview();
  }

  preview() {
    // Show preview
    var mimeType = this.newPersonPicture.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(this.newPersonPicture);
    reader.onload = _event => {
      this.previewUrl = reader.result;
    };
  }

  ngOnInit() {}

  dataTran(input: Person) {
    this.person.copy(input);
  }
}
