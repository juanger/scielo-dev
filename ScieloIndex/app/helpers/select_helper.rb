module SelectHelper
  def simple_select(model,field=nil)
    attribute_id = field || Inflector.foreign_key(model)
    collection = model.find(:all).collect{|record|[record.name, record.id]}
    select("institution", attribute_id, collection, { :prompt => 'Seleccionar' })
  end
end
