import { FormGroup, FormControl, FormBuilder, AbstractControl } from '@angular/forms';

export type FormGroupControlsOf<T> = {
    [P in keyof T]: FormControl | FormGroup;
};

export abstract class FormGroupTypeSafe<T> extends FormGroup {
    //give the value a custom type s
    value: T;

    //create helper methods to achieve this syntax eg: this.form.getSafe(x => x.heroName).patchValue('Himan')
    public abstract getSafe(propertyFunction: (typeVal: T) => any): AbstractControl;
    public abstract setControlSafe(propertyFunction: (typeVal: T) => any, control: AbstractControl): void;
    public abstract resetSafe(typeVal: T): void;
    public abstract patchValueSafe(propertyFunction: (typeVal: T) => any, value: any, options?: {
        onlySelf?: boolean;
        emitEvent?: boolean;
    }): void;
    //If you need more function implement declare them here but implement them on FormBuilderTypeSafe.group instantiation.
}

export class FormControlTypeSafe<T> extends FormControl {
    value: T;
}

export class FormBuilderTypeSafe extends FormBuilder {
    //override group to be type safe
    group<T>(controlsConfig: FormGroupControlsOf<T>, extra?: {
        [key: string]: any;
    } | null): FormGroupTypeSafe<T> {/*NOTE the return FormGroupTypeSafe<T> */

        //instantiate group from angular type
        let gr = super.group(controlsConfig, extra) as FormGroupTypeSafe<T>;

        let getPropertyName = (propertyFunction: Function): string => {
            let properties = propertyFunction.toString().match(/(?<=[.])([a-z0-9_]+)/gi);
            // This regex does not work
            //let properties = propertyFunction.toString().match(/(?![. ])([a-z0-9_]+)(?=[};.])/gi).splice(1);

            var r = properties.join(".");
            //console.info('Property name:', getPropertyName);
            return r;
        }

        if (gr) {
            //implement getSafe method
            gr.getSafe = (propertyFunction: (typeVal: T) => any): AbstractControl => {
                let getStr = getPropertyName(propertyFunction);
                let p = gr.get(getStr) as FormGroupTypeSafe<T>;
                return p;
            }

            //implement setControlSafe 
            gr.setControlSafe = (propertyFunction: (typeVal: T) => any, control: AbstractControl): void => {
                let getStr = getPropertyName(propertyFunction);
                gr.setControl(getStr, control);
            }

            gr.resetSafe = (typeVal: T): void => {
                gr.reset(typeVal);
            };

            gr.patchValueSafe = (propertyFunction: (typeVal: T) => any, value: any, options?: {
                onlySelf?: boolean;
                emitEvent?: boolean;
            }): void => {
                let getStr = getPropertyName(propertyFunction);
                gr.get(getStr).patchValue(value, options);
            }

            //implement more functions as needed           
        }

        return gr;
    }
}