import { Component, ViewChild, ElementRef } from "@angular/core";
import { FormControl, FormGroup, Validators, FormGroupDirective } from '@angular/forms';
import { Router } from "@angular/router";
import { DatepickerModule } from 'angular2-material-datepicker'
import { IMyOptions } from 'mydatepicker';

import { PersonService, SecurityService } from "../../../services";
import { PersonDto } from "../../../dtos";


@Component({
    selector: "person",
    template: require("./add-person.component.html")
})
export class AddPersonComponent {

    constructor(private router: Router,
        private personService: PersonService,
        private securityService: SecurityService) {
    }

    person = new PersonDto();
    private myDatePickerOptions: IMyOptions = {
        dateFormat: 'dd.mm.yyyy',
    };

    addPerson() {
        this.personService.addPerson(this.person).subscribe(
            value => {
                this.router.navigate(["person/view"]);
            },
            error => {
                this.router.navigateByUrl("person/view");
            });
    }
}
