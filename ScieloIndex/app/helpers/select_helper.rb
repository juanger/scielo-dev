module SelectHelper
  def simple_select(form, model, field=nil)
    attribute_id = field || Inflector.foreign_key(model)
    collection = model.find(:all).collect{|record|[record.name, record.id]}
    select(form, attribute_id, collection, { :prompt => 'Seleccionar' })
  end

#   def select_by_attribute(form, model, attribute)
#     attribute_id = Inflector.foreign_key(model)
#     collection = model.find(:all).collect{|record|[record.send(attribute), record.id]}
#     select(form, attribute_id, collection, { :prompt => 'Seleccionar' })
#   end

  def select_by_attribute(form, model, attribute, attribute_id=nil)
    attribute_id ||= Inflector.foreign_key(model)
    collection = model.find(:all).collect{|record|[record.send(attribute), record.id]}
    select(form, attribute_id, collection, { :prompt => 'Seleccionar' })
  end

  def select_journal_issues(form)
    collection = JournalIssue.find(:all).collect{|record| [record.journal.title + '/' + record.number + '-' + record.volume, record.id]}
    select(form, "journal_issue_id", collection, { :prompt => 'Seleccionar' })
  end

  # FIXME: Se usan atributos que pueden ser nulos como suffix y posiblemente middlename.
  def select_authors(form)
    collection = Author.find(:all).collect{|record| [insert_not_nil(record.degree) + record.firstname + ' ' + record.lastname, record.id]}
    select(form, "author_id", collection, { :prompt => 'Seleccionar' })
  end
  
  def insert_not_nil(value)
    if value != nil
      value + ' '
    else
      ''
    end
  end
end
