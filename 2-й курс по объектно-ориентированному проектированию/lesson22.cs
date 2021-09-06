/**
 * Сущность Ит проект
 * Там одновременно есть - тип проекта (t2m, фикс)
 * язык разработки
 */
 
 
public abstract class ProjectType {};
public class TimeToMaterial: ProjectType {};
public class Fix: ProjectType {};


public abstract class Language {};
public class PHP: Language {};
public class RUBY: Language {};

public class Project 
{
 public ProjectType ProjectType { get;set; }
 public Language Language { get;set; }
}