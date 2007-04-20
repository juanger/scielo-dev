require "#{File.dirname(__FILE__)}/../test_helper"

class InstitutionTest < ActionController::IntegrationTest
  fixtures :institutions
  def setup
    @institutions = [:unam, :ipn]
  end

   def test_getting_index
     get "/institutions"
     assert_equal 200, status
     assert_equal '/institutions', path
   end

   def test_new
     get "/institutions/new"
     assert_equal 200, status
     assert_equal '/institutions/new', path
   end
   
   def  test_creating_new_institutions
     post "institutions/create", :institution => institutions(:unam).attributes
#:institution =>  {:id => 1, :name => 'Universidad Nacional Autónoma de México', :url => 'http://www.unam.mx', :abbrev => 'UNAM', :parent_id => 1, :address => 'Ciudad Universitaria, Delegación Coyoacán', :country_id => 484, :state => 'Distrito Federal', :city => 'México', :zipcode => '04510', :phone => '56223040', :fax => '56225678', :other => 'algo'}
     assert_equal 200, status
     puts path
#     follow_redirect!
#     assert_equal '/institutions/list', path
   end

   def test_showing
     @institutions.each { | institution |
       post "/institutions/show", :id => institutions(institution).id
       assert 200, status
       assert_equal '/institutions/show', path
     }
   end

   def test_editing
     @institutions.each { | institution |
       post "/institutions/edit", :id => institutions(institution).id
       assert 200, status
       assert "/institutions/edit/#{institutions(institution).id}", path
     }
   end

   def test_updating_name
     @institutions.each { | institution |
       post "/institutions/update", :id => institutions(institution).id, :name => institutions(institution).name.reverse
       assert 302, status
       follow_redirect!
       assert "/institutions/show/#{institutions(institution).id}", path
     }
   end

    def test_deleting
     @institutions.each { | institution |
       get "/institutions/destroy", :id => institutions(institution).id
       assert 302, status
       assert '/institutions/list',  path
     }
   end
end
