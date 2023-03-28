namespace ioList.Generation.Steps;

public interface IGeneratorStep
{
    void Execute(GeneratorContext context);
}