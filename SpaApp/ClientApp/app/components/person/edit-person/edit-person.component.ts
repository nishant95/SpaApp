import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, FormGroupDirective } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { IMyOptions, IMyDate } from 'mydatepicker';

import { PersonService, SecurityService } from "../../../services";
import { PersonDto } from "../../../dtos";

@Component({
    template: require('./edit-person.component.html')
})
export class EditPersonComponent {
    constructor(private activatedRoute: ActivatedRoute,
        private personService: PersonService,
        private securityService: SecurityService) { }

    personId: number;
    sub: any;
    person: PersonDto = new PersonDto();
    private selDate: IMyDate;

    private myDatePickerOptions: IMyOptions = {
        // other options...
        dateFormat: 'dd.mm.yyyy',
    };

    ngOnInit() {
        this.sub = this.activatedRoute.params.subscribe((params: Params) => {
            this.personId = params['id'];

            this.personService.getPerson(this.personId).subscribe(
                value => {
                    this.person = <PersonDto>value.json();
                    var date = new Date(this.person.Dob);
                    this.selDate = {
                        year: date.getFullYear(),
                        month: date.getMonth() + 1,
                        day: date.getDate()
                    }
                },
                error => {
                    debugger;
                }
            );
        })
    }

    updatePerson() {
        if (this.person.Id == this.personId)
        this.personService.updatePerson(this.person).subscribe(
            value => {
                debugger;
            },
            error => {
                debugger;
            }
        );
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}