module SelectHelper
  def simple_select(form, model, field=nil)
    attribute_id = field || Inflector.foreign_key(model)
    collection = model.find(:all).collect{|record|[record.name, record.id]}
    select(form, attribute_id, collection, { :prompt => 'Seleccionar' })
  end
end
