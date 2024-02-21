export class ConfigurationService {

    public async configuration() {
        const result = await fetch('../../configs/config.json');
        return result.json();
    }
}