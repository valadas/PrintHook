export class PrintClient{
    private readonly basePath: string;

    constructor(port?: number) {
        this.basePath = `http://localhost:${port || 9000}/api/Print/`;
    }

    public async print(data: ILabelField[]): Promise<void> {
        const path = `${this.basePath}Print`;
        let formData = new FormData();
        data.forEach(field => {
            formData.append(field.name, field.value);
        });
        await fetch(
            path,
            {
                method: 'POST',
                body: formData
            });
    }
};

export interface ILabelField {
    name: string;
    value: string;
};