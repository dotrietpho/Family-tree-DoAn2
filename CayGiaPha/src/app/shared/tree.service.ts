import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Person } from "./models/Person";
import { node } from "./models/node";
import { BehaviorSubject } from "rxjs";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Injectable({
  providedIn: "root"
})
export class TreeService {
  tree = new BehaviorSubject<node>(new node());

  hostURL: string = "https://localhost:44360/";
  apiURL: string = "https://localhost:44360/api";

  constructor(private httpClient: HttpClient) {
    this.fetchTree();
  }

  public fetchTree() {
    this.httpClient
      .get<node>(`${this.apiURL}/nodes/1`)
      .toPromise()
      .then(data => {
        this.tree.next(data);
      });
  }

  public createPerson(person: Person) {}

  public updatePerson(person: Person) {
    this.httpClient
      .put(`${this.apiURL}/person/` + person.personId, person)
      .toPromise()
      .then(data => {
        console.log(data);
        this.fetchTree();
      })
      .catch(error => {
        console.log(error);
      });
  }

  public deletePerson(id: number) {
    this.httpClient
      .delete(`${this.apiURL}/person/` + id)
      .toPromise()
      .then(data => {
        console.log(data);
        this.fetchTree();
      })
      .catch(error => {
        console.log(error);
      });
  }

  public getPersonById(id: number) {}

  public getTree(url?: string) {
    return this.tree.asObservable();
  }

  //Them con vao duoi node dang duoc chon
  public addChild(currentId: number, child: Person, uploadData: FormData) {
    this.httpClient
      .put(`${this.apiURL}/AddChild/` + currentId, child)
      .toPromise()
      .then((data: number) => {
        this.uploadFile(data, uploadData);
        this.fetchTree();
      })
      .catch(error => {
        console.log(error);
      });
  }
  //Them cha len tren node dang duoc chon
  public addParent(currentId: number, parent: Person, uploadData: FormData) {
    this.httpClient
      .put(`${this.apiURL}/AddParent/` + currentId, parent)
      .toPromise()
      .then((data: number) => {
        this.uploadFile(data, uploadData);
        this.fetchTree();
      })
      .catch(error => {
        console.log(error);
      });
  }
  //Them partner (Vo/Chong/GulrFirend???) vao node dang duoc chon
  public addPartner(currentId: number, partner: Person, uploadData: FormData) {
    this.httpClient
      .put(`${this.apiURL}/AddPartner/` + currentId, partner)
      .toPromise()
      .then((data: number) => {
        this.uploadFile(data, uploadData);
        this.fetchTree();
      })
      .catch(error => {
        console.log(error);
      });
  }
  //Upload anh vao thu muc UploadedFiles trong server
  public uploadFile(personId: number, uploadData: FormData) {
    this.httpClient
      .post(`${this.hostURL}/Upload/Index?personId=${personId}`, uploadData)
      .toPromise()
      .then(() => {
        this.fetchTree();
      });
  }
}
