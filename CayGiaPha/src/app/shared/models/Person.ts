export class Person {
  personId: number;
  personName: string;
  personGender: string;
  personImg: string;
  IdNodeCha: Number;

  constructor() {
    this.personId = 0;
    this.personName = "";
    this.personGender = "";
    this.personImg = "";
    this.IdNodeCha = 0;
  }

  copy(person: Person) {
    this.personId = person.personId;
    this.personName = person.personName;
    this.personGender = person.personGender;
    this.personImg = person.personImg;
    this.IdNodeCha = person.IdNodeCha;
  }
}
