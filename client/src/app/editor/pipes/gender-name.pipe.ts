import { Pipe, PipeTransform } from "@angular/core";
import { Gender } from "../models/gender.enum";

@Pipe({
    name: "genderName"
})
export class GenderNamePipe implements PipeTransform
{
    private genders: Map<Gender, string> = new Map<Gender, string>([
        [Gender.Male, "мужской"],
        [Gender.Female, "женскй"],
        [Gender.Neutral, "средний"],
        [Gender.Plural, "много"],
    ]);

    transform(gender: Gender): string
    {
        return this.genders.has(gender) ? this.genders.get(gender) :  "# # #";
    }
}
