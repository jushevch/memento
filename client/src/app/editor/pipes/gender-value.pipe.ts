import { Pipe, PipeTransform, Injectable } from "@angular/core";
import { EditorServicesModule } from "../editor-services.module";
import { Gender } from "../models/gender.enum";

@Pipe({
    name: "genderValue"
})
@Injectable({
    providedIn: EditorServicesModule
})
export class GenderValuePipe implements PipeTransform
{
    private names: Map<string, Gender> = new Map<string, Gender>([
        ["мужской", Gender.Male],
        ["женскй", Gender.Female],
        ["средний", Gender.Neutral],
        ["много", Gender.Plural],
    ]);

    transform(name: string): Gender
    {
        return this.names.has(name) ? this.names.get(name) : Gender.None;
    }
}
