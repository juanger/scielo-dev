module QueryHelper
  def postgres?(sql_expression,default="")
    if ActiveRecord::Base.connection.adapter_name == 'PostgreSQL'
      sql_expression
    else
      default
    end
  end
end