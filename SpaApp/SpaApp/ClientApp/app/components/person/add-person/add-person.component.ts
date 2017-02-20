import { Component, ViewChild, ElementRef } from "@angular/core";
import { Router } from "@angular/router";
import { DatepickerModule } from 'angular2-material-datepicker'
import { PersonService } from "../../../services";
import { SecurityService } from "../../../services";
import { PersonDto } from "../../../dtos";

@Component({
    selector: "person",
    template: require("./add-person.component.html")
})
export class AddPersonComponent {
    constructor(private router: Router,
        private personService: PersonService,
        private securityService: SecurityService) { }

    @ViewChild("personDob") personDob: ElementRef;

    person = new PersonDto();

    addPerson() {
        this.personService.addPerson(this.person).subscribe(
            value => {
                this.router.navigateByUrl("person/view");
            },
            error => {
                this.router.navigateByUrl("person/view");
            });
    }

    ngAfterViewInit() {
        //console.log($(this.personDob.nativeElement).val());
    }
}
