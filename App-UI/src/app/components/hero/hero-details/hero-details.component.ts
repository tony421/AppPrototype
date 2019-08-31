import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { Address, Hero, states } from 'src/app/models/hero';
import { FormGroupTypeSafe, FormBuilderTypeSafe } from 'src/app/helpers/reactive-form-helper';
import { FormControl, FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { HeroService } from 'src/app/services/hero.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

interface IHeroFormModel {
    name: string,
    secretLairs: Array<IAddress>,
    power: string,
    sidekick: boolean
}

interface IAddress {
    street: string,
    city: string,
    state: string,
    zip: string
}

@Component({
    selector: 'app-hero-details',
    templateUrl: './hero-details.component.html',
    styleUrls: ['./hero-details.component.scss']
})
export class HeroDetailsComponent implements OnInit, OnChanges {
    @Input() hero: Hero;
    @Output() onSucceeded = new EventEmitter<boolean>();

    /* TypeSafe Reactive Forms Changes */
    //old code - heroForm: FormGroup;
    heroForm: FormGroupTypeSafe<IHeroFormModel>;
    nameChangeLog: string[] = [];
    states = states;

    constructor(
        /* TypeSafe Reactive Forms Changes */
        //old code - private fb: FormBuilder,
        private fb: FormBuilderTypeSafe,
        private heroService: HeroService) {
        this.createForm();
        this.logNameChange();
    }

    ngOnInit() {
    }

    ngOnChanges() {
        this.heroForm.reset(this.hero);
        
        // patchValueSafe is alternative but having issue with multiple resets        
        //this.heroForm.patchValueSafe(x => x.name, this.hero.name);
        //this.heroForm.patchValueSafe(x => x.power, this.hero.power);
        //this.heroForm.patchValueSafe(x => x.sidekick, this.hero.sidekick);

        this.setAddresses(this.hero.addresses);
    }

    createForm() {
        /* TypeSafe Reactive Forms Changes */

        /* old code
        this.heroForm = this.fb.group({
          name: '',
          secretLairs: this.fb.array([]),
          power: '',
          sidekick: ''
        });
        */

        this.heroForm = this.fb.group<IHeroFormModel>({
            name: new FormControl(''),
            secretLairs: new FormControl([]),
            power: new FormControl(''),
            sidekick: new FormControl('')
        });
    }

    createAddressForm(address?: Address): FormGroupTypeSafe<IAddress> {
        return this.fb.group<IAddress>({
            street: new FormControl(address ? address.street : ''),
            city: new FormControl(address ? address.city : ''),
            state: new FormControl(address ? address.state : ''),
            zip: new FormControl(address ? address.zip : '')
        });
    }

    get secretLairs(): FormArray {
        /* TypeSafe Reactive Forms Changes */

        // old code
        /*return this.heroForm.get('secretLairs') as FormArray;*/
        return this.heroForm.getSafe(x => x.secretLairs) as FormArray;
    };

    setAddresses(addresses: Address[]) {
        const addressFGs = addresses.map(address => this.createAddressForm(address));
        const addressFormArray = this.fb.array(addressFGs);

        /* TypeSafe Reactive Forms Changes */
        /*this.heroForm.setControl('secretLairs', addressFormArray); */
        this.heroForm.setControlSafe(x => x.secretLairs, addressFormArray);

    }

    formatAddressToOneLine(addressIndex: number): string {
        let address = this.heroForm.value.secretLairs[addressIndex];
        return address.street + ", " + address.city + ", " + address.state;
    }

    addLair() {
        this.secretLairs.push(this.createAddressForm());
    }

    onSubmit() {
        this.hero = this.prepareSaveHero();
        this.heroService.updateHero(this.hero).subscribe(
            (res:any) => {
                this.onSucceeded.emit(true);
            },
            err => {
                this.onSucceeded.emit(false);
            }
        );
        this.ngOnChanges();
    }

    prepareSaveHero(): Hero {
        const formModel = this.heroForm.value;

        // deep copy of form model lairs
        const secretLairsDeepCopy: Address[] = formModel.secretLairs /*<-- FormGroupTypeSafe form will give you intellisense here*/
            .map(
                (address: Address) => Object.assign({}, address)
            );

        // return new `Hero` object containing a combination of original hero value(s)
        // and deep copies of changed form model values
        const saveHero: Hero = {
            id: this.hero.id,
            name: formModel.name as string, // <-- FormGroupTypeSafe form will give you intellisense here 
            power: formModel.power,
            sidekick: formModel.sidekick,
            // addresses: formModel.secretLairs // <-- bad!
            addresses: secretLairsDeepCopy
        };
        return saveHero;
    }

    revert() { this.ngOnChanges(); }

    cancel() { this.onSucceeded.emit(false); }

    patch() { this.heroForm.patchValueSafe(x => x.name, "xXx"); }
    //patch() { this.heroForm.get('name').patchValue('xXx', { onlySelf: true, eventEmit: false }); }

    logNameChange() {
        /* TypeSafe Reactive Forms Changes */
        const nameControl = this.heroForm.getSafe(x => x.name);
        nameControl.valueChanges.subscribe(
            (value: string) => this.nameChangeLog.push(value)
        );
    }
}
