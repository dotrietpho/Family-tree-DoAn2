import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FamilyTreeComponent } from "./family-tree/family-tree.component";
import { FtLeafComponent } from "./family-tree/ft-leaf.component";
import { InputSectionComponent } from "./input-section/input-section.component";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    FamilyTreeComponent,
    FtLeafComponent,
    InputSectionComponent
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
